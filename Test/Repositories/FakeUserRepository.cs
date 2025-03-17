using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Test.Repositories;

public class FakeUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    public Task CreateAsync(User entity, CancellationToken cancellationToken)
    {
        _users[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task<User> CreateReturnEntity(User entity, CancellationToken cancellationToken)
    {
        _users[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public void Update(User entity)
    {
        if (_users.ContainsKey(entity.Id))
        {
            _users[entity.Id] = entity;
        }
    }

    public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        _users.TryRemove(entity.Id, out _);
        return Task.CompletedTask;
    }

    public Task<List<User>> GetAll(CancellationToken cancellationToken)
        => Task.FromResult(_users.Values.ToList());

    public Task<User?> GetById(Guid? id, CancellationToken cancellationToken)
    {
        if (id == null || !_users.ContainsKey(id.Value))
            return Task.FromResult<User?>(null);

        return Task.FromResult<User?>(_users[id.Value]);
    }

    public Task<User?> GetWithParametersAsync(
        Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<User, object>>[] includes)
    {
        var query = _users.Values.AsQueryable();
        return Task.FromResult(filter != null ? query.FirstOrDefault(filter) : null);
    }

    public Task<List<User>> GetAllWithParametersAsync(
        Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<User, object>>[] includes)
    {
        var query = _users.Values.AsQueryable();
        return Task.FromResult(filter != null ? query.Where(filter).ToList() : query.ToList());
    }

    public Task<List<TResult>> GetAllProjectedAsync<TResult>(
        Expression<Func<User, bool>>? filter = null,
        Expression<Func<User, TResult>> selector = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<User, object>>[] includes)
    {
        var query = _users.Values.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return Task.FromResult(selector != null ? query.Select(selector).ToList() : new List<TResult>());
    }

    public Task<TResult> GetProjectedAsync<TResult>(
        Expression<Func<User, bool>>? filter = null,
        Expression<Func<User, TResult>> selector = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<User, object>>[] includes)
    {
        var query = _users.Values.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return Task.FromResult(selector != null ? query.Select(selector).FirstOrDefault() : default);
    }

    public Task<List<User>> GetAllPages(
        int page,
        int pageSize,
        Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<User, object>>[] includes)
    {
        IEnumerable<User> query = _users.Values.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter.Compile());
        }

        query = query
            .OrderByDescending(u => u.CreatedDate) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return Task.FromResult(query.ToList());
    }

    public Task<bool> Authenticate(User user, CancellationToken cancellationToken)
    {
        var userFromDb = _users.Values.FirstOrDefault(u => u.Email.Address == user.Email.Address && u.Active);

        if (userFromDb == null)
            return Task.FromResult(false);

        var isAuthenticated = userFromDb.Password.VerifyPassword(user.Password.Content, userFromDb.Password.Salt);
        return Task.FromResult(isAuthenticated);
    }

    public Task<User?> ActivateUserAsync(string email, long token, CancellationToken cancellationToken)
    {
        var user = _users.Values.FirstOrDefault(x =>
            !x.Active && x.Email.Address == email && x.TokenActivate == token);

        if (user != null)
        {
            user.AssignActivate(true);
            Update(user);
            return Task.FromResult(user);
        }

        return Task.FromResult(user);
    }

    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var exists = _users.Values.FirstOrDefault(x => x.Email.Address == email);
        return Task.FromResult<User?>(exists);
    }
}
