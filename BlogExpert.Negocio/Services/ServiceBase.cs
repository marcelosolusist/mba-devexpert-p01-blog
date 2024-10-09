using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace BlogExpert.Negocio.Services
{
    public abstract class ServiceBase
    {
        private readonly INotificador _notificador;

        public ServiceBase(INotificador notificador)
        {
            _notificador = notificador;            
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificar(item.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Adicionar(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
