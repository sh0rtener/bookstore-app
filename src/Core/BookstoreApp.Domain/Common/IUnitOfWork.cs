namespace BookstoreApp.Domain.Common;

public interface IUnitOfWork
{
    // Без типов, самая стандартная read commited транзакция 
    void Begin();
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task ReverseAsync(CancellationToken cancellationToken = default);
}
