using BlogExpert.Negocio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExpert.Dados.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(autor => autor.Id);

            builder.Property(autor => autor.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(autor => autor.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(autor => autor.Posts)
                .WithOne(post => post.Autor)
                .HasForeignKey(post => post.AutorId);

            builder.ToTable("Posts");
        }
    }
}
