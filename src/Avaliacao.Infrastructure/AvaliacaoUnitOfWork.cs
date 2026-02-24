using Avaliacao.Domain.Interfaces.Infrastructure;
using Avaliacao.Infrastructure.Context;

namespace Avaliacao.Infrastructure
{
    public class AvaliacaoUnitOfWork(DbContextAvaliacao context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}