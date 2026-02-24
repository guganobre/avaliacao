using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Infrastructure;
using Avaliacao.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Avaliacao.Infrastructure.Repositories
{
    public class SeguroRepository : BaseRepository<Seguro>, ISeguroRepository
    {
        private readonly DbContextAvaliacao _dbContext;

        public SeguroRepository(DbContextAvaliacao context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Seguro>> GetAllWithIncludesAsync()
        {
            return await _dbContext.Seguros
                .Include(s => s.Veiculo)
                .Include(s => s.Segurado)
                .ToListAsync();
        }
    }
}
