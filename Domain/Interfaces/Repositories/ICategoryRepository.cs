using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces;

public interface ICategoryRepository : IClickHouseBaseRepository<Category>
{
    Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken);
}
