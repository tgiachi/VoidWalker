using System.Linq.Expressions;
using VoidWalker.Engine.Database.Interfaces.Entities;

namespace VoidWalker.Engine.Database.Interfaces.Dao;

public interface IBaseDataAccess<TEntity> where TEntity : class, IBaseDbEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<List<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default
    );

    IQueryable<TEntity> AsQueryable(CancellationToken cancellationToken = default);

    Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<long> CountAsync(CancellationToken cancellationToken = default);

    Task<long> CountByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
