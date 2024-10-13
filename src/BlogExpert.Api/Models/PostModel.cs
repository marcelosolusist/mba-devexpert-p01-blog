using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogExpert.Api.Models
{
    public class PostModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid AutorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(4000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [HiddenInput]
        public DateTime? DataCriacao { get; set; }

        [HiddenInput]
        public string? EmailCriacao { get; set; }

        public AutorModel? Autor { get; set; }

        public IEnumerable<ComentarioModel>? Comentarios { get; set; }
    }
}
