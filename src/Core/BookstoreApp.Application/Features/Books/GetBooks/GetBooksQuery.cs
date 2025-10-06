using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;

namespace BookstoreApp.Application.Features.Books.GetBooks;

public class GetBooksQuery : IQuery<IEnumerable<BookDto>>
{
    public BookFilter Filter { get; set; }

    public GetBooksQuery(BookFilter filter)
    {
        Filter = filter;
    }
}

public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> Handle(
        GetBooksQuery command,
        CancellationToken cancellationToken = default
    )
    {
        var books = await _bookRepository.GetBooksAsync(command.Filter, cancellationToken);
        return books.Select(book => new BookDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            PagesCount = book.PagesCount,
            Status = book.Status.Name,
        });
    }
}
