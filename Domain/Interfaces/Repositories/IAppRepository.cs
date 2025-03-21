using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public  interface IAppRepository : IClickHouseBaseRepository<App>
{
    Task<App> GetByName(string name, CancellationToken cancellationToken);
}
