using Avaliacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Infrastructure.Context.Configurations
{
    internal class SeguroConfiguration : IEntityTypeConfiguration<Seguro>
    {
        public void Configure(EntityTypeBuilder<Seguro> builder)
        {
            builder.ToTable("Seguro").HasComment("Tabela de seguros");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired()
                .HasComment("Identificador único do seguro");

            builder.Property(s => s.CriadoEmUtc)
                .IsRequired()
                .HasComment("Data e hora de criação do seguro em UTC");

            // Propriedades de valores
            builder.Property(s => s.TaxaRisco)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Taxa de risco aplicada");

            builder.Property(s => s.PremioRisco)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Prêmio de risco calculado");

            builder.Property(s => s.PremioPuro)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Prêmio puro calculado");

            builder.Property(s => s.PremioComercial)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Prêmio comercial final");

            builder.Property(s => s.ValorSeguro)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Valor total do seguro");

            // Chave estrangeira para Veículo
            builder.Property(s => s.VeiculoId)
                .IsRequired()
                .HasComment("Identificador do veículo segurado");

            // Relacionamento com Veículo
            builder.HasOne(s => s.Veiculo)
                .WithMany()
                .HasForeignKey(s => s.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chave estrangeira para Segurado
            builder.Property(s => s.SeguradoId)
                .IsRequired()
                .HasComment("Identificador do segurado");

            // Relacionamento com Segurado
            builder.HasOne(s => s.Segurado)
                .WithMany()
                .HasForeignKey(s => s.SeguradoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
