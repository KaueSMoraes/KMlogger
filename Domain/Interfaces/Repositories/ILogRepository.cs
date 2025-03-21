using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ILogRepository : IClickHouseBaseRepository<LogEnrty>
{
    Task<IEnumerable<LogEnrty>> GetByIntervalAsync(DateTime dateInitial, DateTime dateFinal, CancellationToken cancellationToken);
    Task<IEnumerable<LogEnrty>> GetByAppAsync(Guid appId, DateTime dateInitial, DateTime dateFinal, CancellationToken cancellationToken);
}