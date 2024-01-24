using Core.Interfaces.Entities.Base;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories.Base;

public interface IGenericBaseRepository<T, TType>
    where T : class, IBaseEntity<TType>
    where TType : struct
{
    Task<T?> GetByIdAsync(TType id);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

    Task<int> AddAsync(T entity);

    Task<int> AddRangeAsync(IEnumerable<T> entities);

    Task<int> UpdateAsync(T entity);

    Task<int> DeleteAsync(TType id);

    Task<int> SaveChangesAsync();
}
