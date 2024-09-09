using Domain.Abstractions;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    protected MainDbContext DbBaseContext { get; }
    protected DbSet<TEntity> Entities { get; }

    protected RepositoryBase(MainDbContext dbContext)
    {
        DbBaseContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Entities = DbBaseContext.Set<TEntity>();
    }

    public IUnitOfWork UnitOfWork => DbBaseContext;
    protected IQueryable<TEntity> Table => Entities;
    protected IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public virtual async Task<TResult> FindFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return await Entities.Where(predicate).AsNoTracking()
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Table.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    public async Task<List<TResult>> FindWithPagination<TResult>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>> include,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default)
        where TResult : class, new()
    {
        var query = TableNoTracking.Where(predicate);

        if (include != null)
        {
            query = include.Compile()(query);
        }

        if (orderBy != null)
        {
            query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        }

        return await query
            .Select(selector)
            .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }


    public virtual void Update(TEntity entity)
    {
        Entities.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }
    public virtual IQueryable<TEntity> Get(List<Expression<Func<TEntity, object>>> includeProperties = null,
        CancellationToken cancellationToken = default)
    {
        if (includeProperties == null) return TableNoTracking.AsQueryable();

        var queryable = TableNoTracking.AsQueryable();
        foreach (var includeProperty in includeProperties)
            queryable = queryable.Include(includeProperty);

        return queryable;
    }
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await TableNoTracking.AnyAsync(predicate, cancellationToken);
    }
}

