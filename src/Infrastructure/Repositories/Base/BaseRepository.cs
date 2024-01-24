using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Interfaces.Repositories.Base;
using Infrastructure.Data;
using Core.Interfaces.Entities.Base;

namespace Infrastructure.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class, IBaseEntity, new()
{
    protected readonly ApplicationDbContext _dataContext;
    private readonly DbSet<T> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _dataContext = context;
        dbSet = _dataContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await dbSet.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate) 
        => await dbSet.Where(predicate).ToListAsync();  
    
    public async Task<int> AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return await SaveChangesAsync();
    }

    public async Task<int> AddRangeAsync(IEnumerable<T> entities)
    {
        await dbSet.AddRangeAsync(entities);
        return await SaveChangesAsync();
    }

    public virtual async Task<int> DeleteAsync(Guid id)
    {
        var entity = await dbSet.FindAsync(id);

        if (entity != null)
        {
            _dataContext.Remove(entity);
            return await SaveChangesAsync();
        }
        return -1;
    }

    public async Task<int> UpdateAsync(T entity)
    {
        _dataContext.Update(entity);
        return await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync() => await _dataContext.SaveChangesAsync();    
}
