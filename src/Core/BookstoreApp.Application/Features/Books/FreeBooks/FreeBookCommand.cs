using BookstoreApp.Application.Features.Books.Exceptions;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Application.Features.Books.FreeBooks;

public class FreeBookCommand : ICommand
{
    public Guid BookId { get; set; }

    public FreeBookCommand(Guid bookId)
    {
        BookId = bookId;
    }
}

public class FreeBookCommandHandler : ICommandHandler<FreeBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public FreeBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(FreeBookCommand command, CancellationToken cancellationToken = default)
    {
        var book =
            await _bookRepository.GetBookAsync(command.BookId, cancellationToken)
            ?? throw new BookWasntFoundException(command.BookId);

        var bookedBook = book.Free();
        await _bookRepository.UpdateStatusAsync(bookedBook, null, null, cancellationToken);
    }
}
