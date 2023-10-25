using DapperTest.Model.context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DapperTest.Model.Repositories;

public interface  ICommandRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> All(params string[] path);
    bool Add(TEntity entity);
    void AddTransaction(TEntity entity);
    Task AddTransactionAsync(TEntity entity);
    Task<bool> AddAsync(TEntity entity);
    bool AddRange(IEnumerable<TEntity> entities);
    Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
    void AddRangeTrans(IEnumerable<TEntity> entities);
    void UpdateRangeTrans(IEnumerable<TEntity> entities);
    Task AddRangeTransAsync(IEnumerable<TEntity> entities);
    bool Remove(TEntity entity);
    void RemoveTrans(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    bool Update(TEntity entity);
    Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);
    void UpdateTransaction(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes);


}

public class CommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : class
{
    ApplicationDbContext _context;
    public CommandRepository(ApplicationDbContext context) => _context = context;
    public IQueryable<TEntity> All(params string[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (path != null)
        {
            foreach (var p in path)
            {
                query = query.Include(p);
            }
        }
        return query;
    }
    public bool Add(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public void AddTransaction(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }
    public async Task AddTransactionAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }
    public bool AddRange(IEnumerable<TEntity> entities)
    {
        try
        {
            _context.Set<TEntity>().AddRange(entities);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        if (await _context.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }

    public void AddRangeTrans(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }
    public void UpdateRangeTrans(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }
    public async Task AddRangeTransAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }
    public async Task<bool> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        if (await _context.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }

    public bool Update(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        if (_context.SaveChanges() > 0)
            return true;
        else
            return false;

    }
    public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.UpdateRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }
    public void UpdateTransaction(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        if (await _context.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }

    public bool Remove(TEntity entity)
    {
        _context.Remove(entity);
        if (_context.SaveChanges() > 0)
            return true;
        else
            return false;
    }

    public void RemoveTrans(TEntity entity)
    {
        _context.Remove(entity);
    }
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
        _context.SaveChanges();
    }
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
        foreach (var i in includes)
        {
            query = query.Include(i);
        }
        return query.Where(predicate);
    }

}
