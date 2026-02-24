using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Infrastructure;
using Avaliacao.Infrastructure.Context;

namespace Avaliacao.Infrastructure.Repositories
{
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(DbContextAvaliacao context) : base(context)
        {
        }
    }
}
