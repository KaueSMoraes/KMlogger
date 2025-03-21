using System;
using Dapper;
using Domain;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CategoryRepository
    : ClickHouseBaseRepository<Category>,
     ICategoryRepository
{
    public async Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        using var connection = CreateConnection();
        string query = $"SELECT * FROM {GetTableName()} WHERE Name = @name LIMIT 1";
        return await connection.QueryFirstOrDefaultAsync<Category>(query, new { name });
    }
}
