using BookstoreApp.Application.Features.Books.Exceptions;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;

namespace BookstoreApp.Application.Features.Books.GetBook;

public class GetBookQuery : IQuery<BookDto>
{
    public Guid Id { get; set; }

    public GetBookQuery(Guid id)
    {
        Id = id;
    }
}

public class GetBookQueryHandler : IQueryHandler<GetBookQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;

    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> Handle(
        GetBookQuery command,
        CancellationToken cancellationToken = default
    )
    {
        var book =
            await _bookRepository.GetBookAsync(command.Id, cancellationToken)
            ?? throw new BookWasntFoundException(command.Id);

        string? username = null;
        DateTime? bookedDate = null;

        if (book.GetType() == typeof(RentedBook))
            username = ((RentedBook)book).UserName;

        if (book.GetType() == typeof(Domain.Books.BookedBook))
        {
            username = ((Domain.Books.BookedBook)book).UserName;
            bookedDate = ((Domain.Books.BookedBook)book).BookedTo;
        }

        return new()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            PagesCount = book.PagesCount,
            Status = book.Status.Name,
            UserName = username,
            BookedTo = bookedDate,
        };
    }
}
