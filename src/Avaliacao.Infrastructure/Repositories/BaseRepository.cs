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

    public Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefaultAsync(predicate);
    }

    public bool Exists(Expression<Func<TEntity, bool>> predicate) => _dbSet.Any(predicate);

    public async Task<IEnumerable<TEntity>> GetListByExpressionAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties
    )
    {
        var query = includeProperties
            .Aggregate<Expression<Func<TEntity, object>>?, IQueryable<TEntity>>(
                _dbSet,
                (current, includeProperty) => current.Include(includeProperty!)
            );

        return await query.Where(predicate).ToListAsync();
    }

    public IQueryable<TEntity> GetListByExpression(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties
    )
    {
        var query = includeProperties
            .Aggregate<Expression<Func<TEntity, object>>?, IQueryable<TEntity>>(
                _dbSet,
                (current, includeProperty) => current.Include(includeProperty!)
            );
        return query.Where(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetListByExpressionNoTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties
    )
    {
        var query = includeProperties
            .Aggregate<Expression<Func<TEntity, object>>?, IQueryable<TEntity>>(
                _dbSet,
                (current, includeProperty) => current.Include(includeProperty!)
            );

        return await query.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(object id, string? className = "")
    {
        return await _dbSet.FindAsync(id) ??
            throw new InvalidOperationException("Não foi possível encontar o objeto na base de dados");
    }

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

    public async Task<TEntity> UpdateAsync(TEntity entity, bool? saveChanges = true)
    {
        var result = _dbSet.Update(entity);
        if (saveChanges.HasValue && saveChanges.Value)
            await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task UpdateEntitiesAsync(params object[] entities)
    {
        foreach (var entity in entities)
        {
            context.Update(entity);
        }
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object id, bool? saveChanges = true, string? className = "")
    {
        var entity = await GetByIdAsync(id, className);

        _dbSet.Remove(entity);

        if (saveChanges.HasValue && saveChanges.Value)
            await context.SaveChangesAsync();
    }

    public void StopTracking(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Detached;
    }
}