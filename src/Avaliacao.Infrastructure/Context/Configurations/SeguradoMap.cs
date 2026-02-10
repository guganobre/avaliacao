using Avaliacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacao.Infrastructure.Context.Configurations
{
    internal class SeguradoConfiguration : IEntityTypeConfiguration<Segurado>
    {
        public void Configure(EntityTypeBuilder<Segurado> builder)
        {
            builder.ToTable("Segurado").HasComment("Tabela de segurados");

            // Chave primária
            builder.HasKey(sg => sg.Id);

            builder.Property(sg => sg.Id)
                .IsRequired()
                .HasComment("Identificador único do segurado");

            builder.Property(sg => sg.Nome)
                .IsRequired()
                .HasMaxLength(200)
                .HasComment("Nome do segurado");

            builder.Property(sg => sg.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .HasComment("CPF do segurado");

            builder.Property(sg => sg.Idade)
                .IsRequired()
                .HasComment("Idade do segurado");
        }
    }
}