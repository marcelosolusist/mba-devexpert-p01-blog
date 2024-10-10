namespace BlogExpert.Negocio.Entities
{
    public class Post : Entity
    {
        public Guid AutorId { get; set; }
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }

        /* EF Relation */
        public Autor? Autor {  get; set; }
        public IEnumerable<Comentario>? Comentarios { get; set; }
    }
}
