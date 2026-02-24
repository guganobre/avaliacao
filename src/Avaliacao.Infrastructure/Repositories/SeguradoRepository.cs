using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Infrastructure;
using Avaliacao.Infrastructure.Context;

namespace Avaliacao.Infrastructure.Repositories
{
    public class SeguradoRepository : BaseRepository<Segurado>, ISeguradoRepository
    {
        public SeguradoRepository(DbContextAvaliacao context) : base(context)
        {
        }
    }
}
