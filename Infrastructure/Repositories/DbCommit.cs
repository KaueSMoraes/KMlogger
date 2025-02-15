using System;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class DbCommit(KMLoggerDbContext context) : IDbCommit
{
    public async Task Commit(CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}