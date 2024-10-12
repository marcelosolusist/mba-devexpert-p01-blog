using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogExpert.Mvc.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Autor")]
        public Guid AutorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(4000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [DisplayName("Data de Postagem")]
        public DateTime? DataCriacao { get; set; }
        
        [HiddenInput]
        public string? EmailCriacao { get; set; }

        public AutorViewModel? Autor { get; set; }

        public IEnumerable<ComentarioViewModel>? Comentarios { get; set; }
    }
}
