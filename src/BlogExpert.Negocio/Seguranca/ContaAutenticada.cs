using BlogExpert.Negocio.Entities.Validations;

namespace BlogExpert.Negocio.Seguranca
{
    public class ContaAutenticada
    {
        public ContaAutenticada(string emailConta, bool ehAdministrador)
        {
            if (string.IsNullOrEmpty(emailConta) || !EmailValidation.EmailEhValido(emailConta)) throw new ArgumentException("O email precisa ser válido.");
            EmailConta = emailConta;
            EhAdministrador = ehAdministrador;
        }
        public string EmailConta { get;  }
        public bool EhAdministrador { get;  }
    }
}
