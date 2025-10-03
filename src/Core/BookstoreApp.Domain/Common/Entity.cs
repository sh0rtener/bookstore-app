namespace BookstoreApp.Domain.Common;

public abstract class Entity<TId>
{
    public TId? Id { get; set; }
}
