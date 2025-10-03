namespace BookstoreApp.Domain.Common;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task ReverseAsync(CancellationToken cancellationToken = default);
}
