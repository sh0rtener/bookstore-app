namespace BookstoreApp.Domain.Books;

public class BookFilter
{
    public string? Author { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public int Limit { get; set; } = 100;
    public int Skip { get; set; } = 0;
}

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(
        BookFilter filter,
        CancellationToken cancellationToken = default
    );

    Task<Book?> GetBookAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddBookAsync(Book book, CancellationToken cancellationToken = default);
    Task UpdateStatusAsync(
        Book book,
        string? userName,
        DateTime? date,
        CancellationToken cancellationToken = default
    );

    Task RemoveBookAsync(Guid id, CancellationToken cancellationToken = default);
}
