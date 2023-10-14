using DataAccessLayer.IRepositories;
using HelperDatas.GlobalReferences;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ImplementDAL.Reporsitory;


public class Reporsitory<TEntity, TPrimaryKeyType> : IRepository<TEntity, TPrimaryKeyType> where TEntity : class
{
    protected readonly DbContext Context;
    public Reporsitory(DbContext context)
    {
        Context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public ValueTask<TEntity> GetByIdAsync(TPrimaryKeyType id)
    {
        return Context.Set<TEntity>().FindAsync(id);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }

    public int Count()
    {
        return Context.Set<TEntity>().Count();
    }

    public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        
        return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }

    public async Task<PagedResult<TEntity>> SearchAndPaginateAsync(Expression<Func<TEntity, bool>> predicate, PaginationOptions options)
    {
        var query = Context.Set<TEntity>().AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        int totalRecords = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalRecords / options.PageSize);
        int startIndex = (options.Page - 1) * options.PageSize;

        List<TEntity> data = await query
            .Skip(startIndex)
            .Take(options.PageSize)
            .ToListAsync();

        return new PagedResult<TEntity>
        {
            Data = data,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };
    }
}
 
