using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class PostService : ServiceBase, IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository, INotificador notificador) : base(notificador)
        {
            _postRepository = postRepository;
        }
        public async Task Adicionar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo).Result.Any())
            {
                Notificar("Já existe um post com o título infomado.");
                return;
            }

            await _postRepository.Adicionar(post);
        }

        public async Task Atualizar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo && p.Id != post.Id).Result.Any())
            {
                Notificar("Já existe um post com o título infomado.");
                return;
            }

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

            await _postRepository.Remover(id);
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }
    }
}
