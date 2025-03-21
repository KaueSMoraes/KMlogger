using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IClickHouseBaseRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken);
    Task SaveAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(string id, T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
