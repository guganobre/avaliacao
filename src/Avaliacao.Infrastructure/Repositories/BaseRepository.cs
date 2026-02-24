using Avaliacao.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public Task<TEntity> FindWithIncludesAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties
    )
    {
        var query = includeProperties
            .Aggregate<Expression<Func<TEntity, object>>?, IQueryable<TEntity>>(
                _dbSet,
                (current, includeProperty) => current.Include(includeProperty!)
            );

        return query.FirstOrDefaultAsync(predicate)!;
    }

    public async Task<TEntity> AddAsync(TEntity entity, bool? saveChanges = true)
    {
        var result = await _dbSet.AddAsync(entity);
        if (saveChanges.HasValue && saveChanges.Value)
            await context.SaveChangesAsync();

        return result.Entity;
    }
}