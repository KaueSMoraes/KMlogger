namespace Domain.Interfaces.Repositories;

public interface IDbCommit
{
    Task Commit(CancellationToken cancellationToken);
}