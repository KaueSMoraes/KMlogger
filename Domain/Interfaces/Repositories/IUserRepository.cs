using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

internal interface IUserRepository : IBaseRepository<User>
{
    Task<bool> Authenticate(User user, CancellationToken cancellationToken);
    Task<bool> ActivateUserAsync(string email, long token, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}