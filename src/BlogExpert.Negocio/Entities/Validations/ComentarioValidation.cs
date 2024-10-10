using FluentValidation;

namespace BlogExpert.Negocio.Entities.Validations
{
    public class ComentarioValidation : AbstractValidator<Comentario>
    {
        public ComentarioValidation()
        {
            RuleFor(comentario => comentario.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 2000).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(comentario => comentario.PostId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
