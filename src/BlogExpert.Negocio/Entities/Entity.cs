using System.ComponentModel.DataAnnotations;

namespace BlogExpert.Negocio.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string? EmailCriacao { get; set; }
    }
}
