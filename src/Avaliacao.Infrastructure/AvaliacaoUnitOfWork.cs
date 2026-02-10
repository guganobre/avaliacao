using Avaliacao.Domain.Interfaces.Infrastructure;
using Avaliacao.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Avaliacao.Infrastructure
{
    public class AvaliacaoUnitOfWork(DbContextAvaliacao context) : IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction ??= await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
                throw new InvalidOperationException("Nenhuma transação ativa para commit.");

            try
            {
                await context.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new BaseRepository<TEntity>(context);
        }
    }
}