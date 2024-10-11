using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogExpert.Mvc.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [HiddenInput]
        public Guid AutorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Conteudo { get; set; }

        public DateTime DataCriacao { get; set; }
        
        [HiddenInput]
        public string EmailCriacao { get; set; }

        public AutorViewModel Autor { get; set; }

        public IEnumerable<AutorViewModel> Autores { get; set; }

        public IEnumerable<ComentarioViewModel> Comentarios { get; set; }
    }
}
