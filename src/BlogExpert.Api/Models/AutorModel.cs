using System.ComponentModel.DataAnnotations;

namespace BlogExpert.Api.Models
{
    public class AutorModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }
    }
}
