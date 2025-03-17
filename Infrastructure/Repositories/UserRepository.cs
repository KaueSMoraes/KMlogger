using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public  class UserRepository(KMLoggerDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> Authenticate(User user, CancellationToken cancellationToken)
    {
        var userFromDb = await context.Set<User>().AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Address == user.Email.Address && u.Active, cancellationToken);
        return userFromDb != null && userFromDb.Password.VerifyPassword(user.Password.Content, userFromDb.Password.Salt);
    }

    public async Task<User> ActivateUserAsync(string email, long token, CancellationToken cancellationToken)
    {
       var user =  (await context.Set<User>().AsNoTracking()
            .FirstOrDefaultAsync(x => !x.Active && x.Email.Address!.Equals(email) && x.TokenActivate.Equals(token),
                cancellationToken: cancellationToken));

       user?.AssignActivate(true);
       Update(user);
       return user!;
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken) =>
        await context.Set<User>().AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address.Equals(email), cancellationToken);
}
