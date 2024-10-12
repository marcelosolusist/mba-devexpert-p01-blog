using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class PostService : ServiceBase, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAutorRepository _autorRepository;
        public PostService(IPostRepository postRepository, IAutorRepository autorRepository, INotificador notificador, IContaAutenticada contaAutenticada) : base(notificador, contaAutenticada)
        {
            _postRepository = postRepository;
            _autorRepository = autorRepository;
        }
        public async Task Adicionar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo).Result.Any())
            {
                Notificar("Já existe post com o título infomado.");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post, true))
            {
                return;
            }

            post.EmailCriacao = _contaAutenticada.Email;

            await _postRepository.Adicionar(post);
        }

        public async Task Atualizar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo && p.Id != post.Id).Result.Any())
            {
                Notificar("Já existe post com o título infomado.");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post, false)) return;

            await _postRepository.Atualizar(post);
        }

        public async Task Remover(Guid id)
        {
            var post = await _postRepository.ObterPorId(id);

            if (post == null)
            {
                Notificar("Post não existe!");
                return;
            }

            if (await _postRepository.VerificarSePossuiComentario(id))
            {
                Notificar("O post possui comentário cadastrado!");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post, false)) return;

            await _postRepository.Remover(id);
        }

        public async Task<List<Autor>> ListarAutoresDaContaAutenticada()
        {

            if (!_contaAutenticada.EhAdministrador) return _autorRepository.Buscar(a => a.Email == _contaAutenticada.Email).Result.ToList();

            return _autorRepository.Buscar(a => !string.IsNullOrEmpty(a.Nome)).Result.OrderBy(a => a.Nome).ToList();
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }

        private async Task<bool> VerificarSeAutorValidoEPodeManipularPost(Post post, bool verificarAtivo)
        {
            var autor = await _autorRepository.ObterPorId(post.AutorId);

            if (autor == null)
            {
                Notificar("O autor informado para o post não existe.");
                return false;
            }

            if (verificarAtivo && !autor.Ativo)
            {
                Notificar("O autor não está ativo.");
                return false;
            }

            if (!_contaAutenticada.EhAdministrador)
            {
                var autoresDaConta = await ListarAutoresDaContaAutenticada();
                if (autoresDaConta == null)
                {
                    Notificar("Não há autor disponível para a conta autenticada.");
                    return false;
                }
                var autorInformado = autoresDaConta.FirstOrDefault(a => a.Id == post.AutorId);
                if (autorInformado == null)
                {
                    Notificar("A conta autenticada não pode manipular os posts do autor informado.");
                    return false;
                }
            }

            if (verificarAtivo || _contaAutenticada.EhAdministrador || post.EmailCriacao == _contaAutenticada.Email || autor.Email == _contaAutenticada.Email) return true;
            
            Notificar("A conta autenticada não pode manipular esse post.");
            return false;
        }

        
    }
}
