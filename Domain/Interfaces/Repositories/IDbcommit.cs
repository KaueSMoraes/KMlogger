namespace Domain.Interfaces.Repositories;

internal interface IDbCommit
{
    Task Commit(CancellationToken cancellationToken);
}