using BlogExpert.Negocio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogExpert.Dados.Mappings
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {

            builder.Property(post => post.Titulo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(post => post.Conteudo)
                .IsRequired()
                .HasColumnType("varchar(4000)");

            builder.Property(post => post.EmailCriacao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(post => post.Comentarios)
                .WithOne(comentario => comentario.Post)
                .HasForeignKey(comentario => comentario.PostId);

            builder.ToTable("Posts");
        }
    }
}
