using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Nest;

namespace Infrastructure.Repositories;

public abstract class ElasticBaseRepository<T>(ElasticClient client, string indexName) 
    : IElasticBaseRepository<T> where T : Entity
{
    public virtual async Task<T> GetByIdAsync(string id)
    {
        var response = await client.GetAsync<T>(id, idx => idx.Index(indexName));
        return response.Source;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await client.SearchAsync<T>(s => s.Index(indexName).MatchAll());
        return response.Documents;
    }

    public virtual async Task SaveAsync(T entity)
        => await client.IndexDocumentAsync(entity);
    
    public virtual async Task UpdateAsync(string id, T entity)
        => await client.UpdateAsync<T>(id, u => u.Index(indexName).Doc(entity));

    public virtual async Task DeleteAsync(string id)
        => await client.DeleteAsync<T>(id, d => d.Index(indexName));
}
