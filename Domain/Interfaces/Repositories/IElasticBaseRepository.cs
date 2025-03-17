using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IElasticBaseRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}
