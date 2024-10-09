namespace BlogExpert.Negocio.Entities
{
    public class Autor : Entity
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }

        /* EF Relation */
        public IEnumerable<Post>? Posts { get; set; }
    }
}
