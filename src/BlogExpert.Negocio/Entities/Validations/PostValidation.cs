using FluentValidation;

namespace BlogExpert.Negocio.Entities.Validations
{
    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor(post => post.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(post => post.Conteudo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 4000).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
