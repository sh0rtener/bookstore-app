using System.Data;
using BookstoreApp.Domain.Books;
using BookstoreApp.Persistense.Daos;
using BookstoreApp.Persistense.Postgres.RawQueries;
using Dapper;

namespace BookstoreApp.Persistense.Dapper.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbConnection _connection;

    public BookRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AddBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _connection.ExecuteAsync(
            BookSqlQueries.AddNewBook,
            new
            {
                Id = Guid.NewGuid(),
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                PagesCount = book.PagesCount,
            }
        );
    }

    public async Task<Book?> GetBookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _connection.QueryFirstOrDefaultAsync<BookDao>(
            BookSqlQueries.GetBookById,
            new { Id = id }
        );

        if (book is null)
            return null;

        switch (book.Status)
        {
            case "Free":
                return new FreeBook(
                    book.Title ?? "",
                    book.Description ?? "",
                    book.PagesCount,
                    book.Author ?? ""
                )
                {
                    Id = book.Id,
                };
            case "Booked":
                return new BookedBook(
                    book.Title ?? "",
                    book.Description ?? "",
                    book.PagesCount,
                    book.Author ?? "",
                    book.UserName ?? "",
                    (DateTime)book.BookedTo!
                )
                {
                    Id = book.Id,
                };
            case "Rent":
                return new RentedBook(
                    book.Title ?? "",
                    book.Description ?? "",
                    book.PagesCount,
                    book.Author ?? "",
                    book.UserName ?? ""
                )
                {
                    Id = book.Id,
                };

            default:
                return null;
        }
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(
        BookFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var books = await _connection.QueryAsync<BookDao>(
            BookSqlQueries.GetBooks,
            new
            {
                Author = string.Format("%{0}%", filter.Author),
                Title = string.Format("%{0}%", filter.Name),
                Status = string.Format("%{0}%", filter.Status),
                Take = filter.Limit,
                Skip = filter.Skip,
            }
        );

        return books.Select<BookDao, Book>(book =>
        {
            switch (book.Status)
            {
                case "Free":
                    return new FreeBook(
                        book.Title ?? "",
                        book.Description ?? "",
                        book.PagesCount,
                        book.Author ?? ""
                    )
                    {
                        Id = book.Id,
                    };
                case "Booked":
                    return new BookedBook(
                        book.Title ?? "",
                        book.Description ?? "",
                        book.PagesCount,
                        book.Author ?? "",
                        book.UserName ?? "",
                        (DateTime)book.BookedTo!
                    )
                    {
                        Id = book.Id,
                    };
                case "Rent":
                    return new RentedBook(
                        book.Title ?? "",
                        book.Description ?? "",
                        book.PagesCount,
                        book.Author ?? "",
                        book.UserName ?? ""
                    )
                    {
                        Id = book.Id,
                    };
                default:
                    return null!;
            }
        });
    }

    public async Task RemoveBookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _connection.ExecuteAsync(BookSqlQueries.Remove, new { Id = id });
    }

    public async Task UpdateStatusAsync(
        Book book,
        string? userName,
        DateTime? date,
        CancellationToken cancellationToken = default
    )
    {
        await _connection.ExecuteAsync(
            BookSqlQueries.UpdateStatus,
            new
            {
                Id = book.Id,
                Status = book.Status.Name,
                UserName = userName,
                BookedTo = date,
            }
        );
    }
}
