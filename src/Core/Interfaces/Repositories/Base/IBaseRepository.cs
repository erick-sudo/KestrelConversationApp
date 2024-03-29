﻿using Core.Interfaces.Entities.Base;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories.Base;

public interface IBaseRepository<T>
    where T : class, IBaseEntity
{
    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

    Task<int> AddAsync(T entity);

    Task<int> AddRangeAsync(IEnumerable<T> entities);

    Task<int> UpdateAsync(T entity);

    Task<int> DeleteAsync(Guid id);

    Task<int> SaveChangesAsync();
}
