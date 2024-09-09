using System.Linq.Expressions;

namespace Domain.Abstractions;

public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    IUnitOfWork UnitOfWork { get; }

    Task<TResult> FindFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<List<TResult>> FindWithPagination<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>> include,
        int pageIndex,
        int pageSize, Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false, CancellationToken cancellationToken = default)
        where TResult : class, new();

    void Update(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Delete(TEntity entity);
    IQueryable<TEntity> Get(List<Expression<Func<TEntity, object>>> includeProperties = null,
        CancellationToken cancellationToken = default);

}
