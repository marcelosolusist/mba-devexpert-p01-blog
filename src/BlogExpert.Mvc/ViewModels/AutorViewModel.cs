using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogExpert.Mvc.ViewModels
{
    public class AutorViewModel
    {

        [Key]
        public Guid Id { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }
    }
}
