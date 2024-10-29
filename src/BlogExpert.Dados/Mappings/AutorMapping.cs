using BlogExpert.Negocio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogExpert.Dados.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.Property(autor => autor.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(autor => autor.EmailCriacao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(autor => autor.Posts)
                .WithOne(post => post.Autor)
                .HasForeignKey(post => post.AutorId);

            builder.ToTable("Autores");
        }
    }
}
