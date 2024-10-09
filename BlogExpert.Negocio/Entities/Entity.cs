namespace BlogExpert.Negocio.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
