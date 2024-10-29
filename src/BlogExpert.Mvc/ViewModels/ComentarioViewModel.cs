using BlogExpert.Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogExpert.Mvc.ViewModels
{
    public class ComentarioViewModel
    {
        private readonly IContaAutenticada _contaAutenticada; 

        [Key]
        public Guid Id { get; set; }

        [HiddenInput]
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(2000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public DateTime? DataCriacao { get; set; }
        [HiddenInput]
        public string? EmailCriacao { get; set; }

        public PostViewModel? Post { get; set; }
        public bool PodeManipular { get; set; }
    }
}
