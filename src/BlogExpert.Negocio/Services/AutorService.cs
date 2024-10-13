using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class AutorService : ServiceBase, IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository, INotificador notificador, IContaAutenticada contaAutenticada) : base(notificador, contaAutenticada)
        {
            _autorRepository = autorRepository;
        }

        public async Task Adicionar(Autor autor)
        {
            if (!ExecutarValidacao(new AutorValidation(), autor)) return;

            var autorDuplicado = _autorRepository.Buscar(a => a.Id == autor.Id);
            if (autorDuplicado.Result.Any())
            {
                Notificar("Já existe um autor com o Id informado.");
                return;
            }

            if (autor.Email != _contaAutenticada.Email)
            {
                Notificar("Só é possível criar um autor com o email da conta autenticada.");
                return;
            }

            autor.EmailCriacao = _contaAutenticada.Email;
            autor.DataCriacao = DateTime.Now;

            if (_autorRepository.Buscar(a => a.Email == autor.Email).Result.Any())
            {
                Notificar("Já existe um autor com o email infomado.");
                return;
            }

            if (!VerificarSePodeManipularAutor(autor)) return;

            await _autorRepository.Adicionar(autor);
        }

        public async Task Atualizar(Autor autor)
        {
            if (!ExecutarValidacao(new AutorValidation(), autor)) return;

            if (_autorRepository.Buscar(a => a.Email == autor.Email && a.Id != autor.Id).Result.Any())
            {
                Notificar("Já existe um autor com o email infomado.");
                return;
            }

            if (!VerificarSePodeManipularAutor(autor)) return;

            await _autorRepository.Atualizar(autor);
        }

        public async Task Remover(Guid id)
        {
            var autor = await _autorRepository.ObterPorId(id);

            if (autor == null)
            {
                Notificar("Autor não existe!");
                return;
            }

            if (await _autorRepository.VerificarSePossuiPost(id))
            {
                Notificar("O autor possui post cadastrado!");
                return;
            }

            if (!VerificarSePodeManipularAutor(autor)) return;

            await _autorRepository.Remover(id);
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }

        private bool VerificarSePodeManipularAutor(Autor autor)
        {
            if (_contaAutenticada.EhAdministrador || autor.Email == _contaAutenticada.Email) return true;
            
            Notificar("A conta autenticada não pode manipular esse autor.");
            return false;
        }

        public async Task<Autor> ObterParaEdicao(Guid id)
        {
            var autor = await _autorRepository.ObterPorId(id);

            if (autor == null)
            {
                Notificar("Autor não existe!");
                return null;
            }

            if (!_contaAutenticada.EhAdministrador && autor.Email != _contaAutenticada.Email)
            {
                Notificar("A conta autenticada não pode manipular o autor selecionado.");
                return null;
            }

            return autor;
        }


    }
}
