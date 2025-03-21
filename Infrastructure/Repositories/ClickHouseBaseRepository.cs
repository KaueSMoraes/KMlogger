using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ClickHouse.Client.ADO;
using Dapper;
using Domain;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public abstract class ClickHouseBaseRepository<T> 
        : IClickHouseBaseRepository<T> where T : Entity
    {
        protected IDbConnection CreateConnection() => new ClickHouseConnection(Configuration.ConnectionStringClickHouse);

        public async Task SaveAsync(T entity, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();
            string query = $"INSERT INTO {GetTableName()} (id, createddate, updateddate, deleteddate) VALUES (@Id, @CreatedDate, @UpdatedDate, @DeletedDate)";
            await connection.ExecuteAsync(query, entity);
        }

        public virtual async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();
            string query = $"ALTER TABLE {GetTableName()} DELETE WHERE id = @id";
            await connection.ExecuteAsync(query, new { id });
            await SaveAsync(entity, cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();
            string query = $"ALTER TABLE {GetTableName()} DELETE WHERE id = @id";
            await connection.ExecuteAsync(query, new { id });
        }

        protected string GetTableName()
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            return tableAttr?.Name ?? typeof(T).Name.ToLower();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();
            string query = $"SELECT * FROM {GetTableName()} WHERE id = @id LIMIT 1";
            return await connection.QueryFirstOrDefaultAsync<T>(query, new { id });
        }

        public async Task<IEnumerable<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();
            string query = $"SELECT * FROM {GetTableName()} ORDER BY createddate DESC LIMIT @take OFFSET @skip";
            return await connection.QueryAsync<T>(query, new { take, skip });
        }
    }
}
