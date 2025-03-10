using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task<T> CreateReturnEntity(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<T> GetWithParametersAsync(Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default,  params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllWithParametersAsync(Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default,  params Expression<Func<T, object>>[] includes);

    Task<List<TResult>> GetAllProjectedAsync<TResult>(Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>> selector = null, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);
    
    Task<TResult> GetProjectedAsync<TResult>(Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>> selector = null, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);
}