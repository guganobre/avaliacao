using Avaliacao.Domain.Entities;
using Avaliacao.Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Avaliacao.Infrastructure.Context
{
    public class DbContextAvaliacao : DbContext
    {
        public DbContextAvaliacao()
        {
        }

        public DbContextAvaliacao(DbContextOptions<DbContextAvaliacao> options)
            : base(options)
        {
        }

        public DbSet<Seguro> Seguros { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Segurado> Segurados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Connection string do docker-compose
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=AvaliacaoDB;User Id=sa;Password=Avaliacao@123;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VeiculoConfiguration());
            modelBuilder.ApplyConfiguration(new SeguradoConfiguration());
            modelBuilder.ApplyConfiguration(new SeguroConfiguration());
        }
    }
}