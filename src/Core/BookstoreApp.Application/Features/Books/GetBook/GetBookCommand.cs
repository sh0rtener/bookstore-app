using BookstoreApp.Application.Features.Books.Exceptions;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;

namespace BookstoreApp.Application.Features.Books.GetBook;

public class GetBookCommand : ICommand<BookDto>
{
    public Guid Id { get; set; }

    public GetBookCommand(Guid id)
    {
        Id = id;
    }
}

public class GetBookCommandHandler : ICommandHandler<GetBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;

    public GetBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> Handle(
        GetBookCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var book =
            await _bookRepository.GetBookAsync(command.Id, cancellationToken)
            ?? throw new BookWasntFoundException(command.Id);

        return new()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            PagesCount = book.PagesCount,
            Status = book.Status.Name,
        };
    }
}
