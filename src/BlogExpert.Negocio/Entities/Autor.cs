namespace BlogExpert.Negocio.Entities
{
    public class Autor : Entity
    {
        public string? Email { get; set; }

        /* EF Relation */
        public IEnumerable<Post>? Posts { get; set; }
    }
}
