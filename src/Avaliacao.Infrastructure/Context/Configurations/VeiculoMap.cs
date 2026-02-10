using Avaliacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacao.Infrastructure.Context.Configurations
{
    internal class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo").HasComment("Tabela de veículos");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .IsRequired()
                .HasComment("Identificador único do veículo");

            builder.Property(v => v.MarcaModelo)
                .IsRequired()
                .HasMaxLength(200)
                .HasComment("Marca e modelo do veículo");

            builder.Property(v => v.Valor)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Valor do veículo");
        }
    }
}