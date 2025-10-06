using System.Data;
using BookstoreApp.Application.Features.Books.AddBook;
using BookstoreApp.Application.Features.Books.BookedBook;
using BookstoreApp.Application.Features.Books.FreeBooks;
using BookstoreApp.Application.Features.Books.GetBook;
using BookstoreApp.Application.Features.Books.GetBooks;
using BookstoreApp.Application.Features.Books.RemoveBook;
using BookstoreApp.Application.Features.Books.RentBook;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Persistense.Dapper.Repositories;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace BookstoreApp.WebApi.Controllers;

[ApiController]
[Route("book")]
public class BookController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(Guid id, CancellationToken cancellationToken)
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, IQueryHandlerBase> queries = new Dictionary<Type, IQueryHandlerBase>
        {
            { typeof(GetBookQuery), new GetBookQueryHandler(repository) },
        };

        var pipeline = new Pipeline(queries: queries);

        var result = await pipeline.Send(new GetBookQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetBooks(
        CancellationToken cancellationToken,
        string author = "",
        string status = "",
        string title = "",
        int limit = 10,
        int skip = 0
    )
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, IQueryHandlerBase> queries = new Dictionary<Type, IQueryHandlerBase>
        {
            { typeof(GetBooksQuery), new GetBooksQueryHandler(repository) },
        };

        var pipeline = new Pipeline(queries: queries);

        var result = await pipeline.Send(
            new GetBooksQuery(
                new BookFilter()
                {
                    Author = author,
                    Name = title,
                    Status = status,
                    Limit = limit,
                    Skip = skip,
                }
            ),
            cancellationToken
        );

        return Ok(result);
    }

    [HttpPost()]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken,
        AddBookCommandIn request
    )
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, ICommandHandlerBase> commands = new Dictionary<Type, ICommandHandlerBase>
        {
            { typeof(AddBookCommand), new AddBookCommandHandler(repository) },
        };

        var pipeline = new Pipeline(commands: commands);

        await pipeline.Send(new AddBookCommand(request), cancellationToken);

        return Accepted();
    }

    [HttpPatch("rent")]
    public async Task<IActionResult> Rent(
        CancellationToken cancellationToken,
        Guid id,
        string userName
    )
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, ICommandHandlerBase> commands = new Dictionary<Type, ICommandHandlerBase>
        {
            { typeof(RentBookCommand), new RentBookCommandHandler(repository) },
        };

        var pipeline = new Pipeline(commands: commands);

        await pipeline.Send(new RentBookCommand(id, userName), cancellationToken);

        return Accepted();
    }

    [HttpPatch("book")]
    public async Task<IActionResult> Book(
        CancellationToken cancellationToken,
        Guid id,
        string userName,
        DateTime date
    )
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, ICommandHandlerBase> commands = new Dictionary<Type, ICommandHandlerBase>
        {
            { typeof(BookedBookCommand), new BookedBookCommandHandler(repository) },
        };

        var pipeline = new Pipeline(commands: commands);

        await pipeline.Send(new BookedBookCommand(id, userName, date), cancellationToken);

        return Accepted();
    }

    [HttpPut("free")]
    public async Task<IActionResult> Free(CancellationToken cancellationToken, Guid id)
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, ICommandHandlerBase> commands = new Dictionary<Type, ICommandHandlerBase>
        {
            { typeof(FreeBookCommand), new FreeBookCommandHandler(repository) },
        };

        var pipeline = new Pipeline(commands: commands);

        await pipeline.Send(new FreeBookCommand(id), cancellationToken);

        return Accepted();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        IDbConnection connection = new NpgsqlConnection(
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;SSL Mode=Disable"
        );
        IBookRepository repository = new BookRepository(connection);

        Dictionary<Type, ICommandHandlerBase> commands = new Dictionary<Type, ICommandHandlerBase>
        {
            { typeof(RemoveBookCommand), new RemoveBookCommandHandler(repository) },
        };

        var pipeline = new Pipeline(commands: commands);

        await pipeline.Send(new RemoveBookCommand(id), cancellationToken);

        return Accepted();
    }
}
