using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Database.Context;
using VoidWalker.Engine.Database.Interfaces.Dao;
using VoidWalker.Engine.Database.Interfaces.Entities;

namespace VoidWalker.Engine.Database.Impl;

public abstract class AbstractBaseDataAccess<TEntity, TDbContext> : IBaseDataAccess<TEntity>
    where TEntity : class, IBaseDbEntity where TDbContext : BaseDbContext
{
    private readonly IDbContextFactory<TDbContext> _contextFactory;
    private readonly ILogger _logger;

    protected AbstractBaseDataAccess(ILogger<TEntity> logger, IDbContextFactory<TDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        await dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        _logger.LogDebug("Entity with id {Id} added", entity.Id);
        return entity;
    }

    public async Task<List<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        await dbSet.AddRangeAsync(entities, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        _logger.LogDebug("Entities added");

        return entities.ToList();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        dbSet.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        _logger.LogDebug("Entity with id {Id} updated", entity.Id);
        return entity;
    }

    public async Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        var entity = await dbSet.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            _logger.LogWarning("Entity with id {Id} not found", id);
            return null;
        }

        dbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        _logger.LogDebug("Entity with id {Id} deleted", id);
        return entity;
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("Entity with id {Id} fetched", id);
        return await dbSet.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("All entities fetched");
        return await dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default
    )
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("Entities fetched by query");
        return await dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> AsQueryable(CancellationToken cancellationToken = default)
    {
        var context = _contextFactory.CreateDbContext();
        var dbSet = context.Set<TEntity>();

        _logger.LogDebug("Queryable entities fetched");
        return dbSet.AsQueryable();
    }

    public async Task<TEntity> GetSingleByAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default
    )
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("Entity fetched by query");
        return await dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("Count of entities fetched");
        return await dbSet.LongCountAsync(cancellationToken);
    }

    public async Task<long> CountByAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default
    )
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("Count of entities fetched by query");
        return await dbSet.LongCountAsync(predicate, cancellationToken);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default
    )
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = context.Set<TEntity>();
        _logger.LogDebug("Check if entities exist by query");
        return await dbSet.AnyAsync(predicate, cancellationToken);
    }
}
