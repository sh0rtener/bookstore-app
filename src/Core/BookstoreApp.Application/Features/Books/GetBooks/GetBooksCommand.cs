using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;

namespace BookstoreApp.Application.Features.Books.GetBooks;

public class GetBooksCommand : ICommand<IEnumerable<BookDto>>
{
    public required BookFilter Filter { get; set; }

    public GetBooksCommand(BookFilter filter)
    {
        Filter = filter;
    }
}

public class GetBooksCommandHandler : ICommandHandler<GetBooksCommand, IEnumerable<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> Handle(
        GetBooksCommand command,
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
