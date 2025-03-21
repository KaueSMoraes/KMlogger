using System;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AppRepository : ClickHouseBaseRepository<App>, IAppRepository
{
    public async Task<App> GetByName(string name, CancellationToken cancellationToken)
    {
        using var connection = CreateConnection();
        string query = $"SELECT * FROM {GetTableName()} WHERE Name = @name LIMIT 1";
        return await connection.QueryFirstOrDefaultAsync<App>(query, new { name });
    }
}
