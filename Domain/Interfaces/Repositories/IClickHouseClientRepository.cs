using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IClickHouseBaseRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task SaveAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(string id, T entity, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
