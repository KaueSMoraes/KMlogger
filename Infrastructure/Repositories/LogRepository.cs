using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;
public class LogRepository : ClickHouseBaseRepository<LogEnrty>, ILogRepository
{
    public async Task<IEnumerable<LogEnrty>> GetByAppAsync(Guid appId, DateTime dateInitial, DateTime dateFinal,
        CancellationToken cancellationToken)
    {
        using var connection = CreateConnection();
        string query = $@"
            SELECT * FROM {GetTableName()} 
            WHERE appId = @appId 
            AND createddate BETWEEN @dateInitial AND @dateFinal 
            ORDER BY createddate DESC";

        return await connection.QueryAsync<LogEnrty>(query, new { appId, dateInitial, dateFinal });
    }

    public async Task<IEnumerable<LogEnrty>> GetByIntervalAsync(DateTime dateInitial, DateTime dateFinal,
        CancellationToken cancellationToken)
    {
        using var connection = CreateConnection();
        string query = $@"
            SELECT * FROM {GetTableName()} 
            WHERE createddate BETWEEN @dateInitial AND @dateFinal 
            ORDER BY createddate DESC";

        return await connection.QueryAsync<LogEnrty>(query, new { dateInitial, dateFinal });
    }
}
