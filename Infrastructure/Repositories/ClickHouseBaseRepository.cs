using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClickHouse.Client.ADO;
using ClickHouse.Client.ADO.Parameters;
using Domain;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public abstract class ClickHouseBaseRepository<T> 
    : IClickHouseBaseRepository<T> where T : Entity
{
    protected readonly string _connectionString = Configuration.ConnectionStringClickHouse;
    public virtual async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        using var connection = new ClickHouseConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $"SELECT * FROM {T.GetType()} WHERE id = @id LIMIT 1";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Parameters.Add(new ClickHouseParameter("@id", id));

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            return MapToEntity(reader);
        }

        return null;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        using var connection = new ClickHouseConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $"SELECT * FROM {_tableName}";

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var results = new List<T>();

        while (await reader.ReadAsync(cancellationToken))
        {
            results.Add(MapToEntity(reader));
        }

        return results;
    }

    public async Task SaveAsync(T entity, CancellationToken cancellationToken)
    {
        using var connection = new ClickHouseConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $"INSERT INTO {_tableName} (id, created_at, data) VALUES (@id, now(), @data)";

        using var command = connection.CreateCommand();
        command.CommandText = query;

        // Criando parâmetros de forma correta
        var idParam = command.CreateParameter();
        idParam.ParameterName = "id";
        idParam.Value = entity.Id;
        command.Parameters.Add(idParam);

        var dataParam = command.CreateParameter();
        dataParam.ParameterName = "data";
        dataParam.Value = entity.ToJson();
        command.Parameters.Add(dataParam);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken)
    {
        await DeleteAsync(id, cancellationToken);
        await SaveAsync(entity, cancellationToken);
    }

    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        using var connection = new ClickHouseConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $"ALTER TABLE {_tableName} DELETE WHERE id = @id";

        using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Parameters.Add(new ClickHouseParameter("@id", id));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private T MapToEntity(IDataReader reader)
    {
        // Implementar a conversão do resultado do ClickHouse para o objeto T
        return new T
        {
            Id = reader["id"].ToString(),
            CreatedAt = DateTime.Parse(reader["created_at"].ToString()),
            Data = reader["data"].ToString()
        };
    }
}
