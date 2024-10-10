using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Seguranca;

namespace BlogExpert.Negocio.Services
{
    public class AutorService : ServiceBase, IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository, INotificador notificador) : base(notificador)
        {
            _autorRepository = autorRepository;
        }

        public async Task Adicionar(Autor autor, ContaAutenticada contaAutenticada)
        {
            if (!ExecutarValidacao(new AutorValidation(), autor)) return;

            if (_autorRepository.Buscar(a => a.Email == autor.Email).Result.Any())
            {
                Notificar("Já existe um autor com o email infomado.");
                return;
            }

            autor.EmailCriacao = contaAutenticada.EmailConta;

            await _autorRepository.Adicionar(autor);
        }

        public async Task Atualizar(Autor autor, ContaAutenticada contaAutenticada)
        {
            if (!ExecutarValidacao(new AutorValidation(), autor)) return;

            if (_autorRepository.Buscar(a => a.Email == autor.Email && a.Id != autor.Id).Result.Any())
            {
                Notificar("Já existe um autor com o email infomado.");
                return;
            }

            if (!VerificarSePodeManipularAutor(autor, contaAutenticada)) return;

            await _autorRepository.Atualizar(autor);
        }

        public async Task Remover(Guid id, ContaAutenticada contaAutenticada)
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

            if (!VerificarSePodeManipularAutor(autor, contaAutenticada)) return;

            await _autorRepository.Remover(id);
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }

        private bool VerificarSePodeManipularAutor(Autor autor, ContaAutenticada contaAutenticada)
        {
            if (contaAutenticada.EhAdministrador || autor.EmailCriacao == contaAutenticada.EmailConta || autor.Email == contaAutenticada.EmailConta) return true;
            
            Notificar("A conta autenticada não pode manipular esse autor.");
            return false;
        }
    }
}
