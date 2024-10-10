namespace BlogExpert.Negocio.Entities
{
    public class Comentario: Entity
    {
        public Guid PostId { get; set; }
        public string? Descricao { get; set; }

        /* EF Relation */
        public Post? Post { get; set; }
    }
}
