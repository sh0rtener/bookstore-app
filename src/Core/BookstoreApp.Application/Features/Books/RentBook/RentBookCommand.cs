using BookstoreApp.Application.Features.Books.Exceptions;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Application.Features.Books.RentBook;

public class RentBookCommand : ICommand
{
    public Guid BookId { get; set; }
    public string UserName { get; set; }

    public RentBookCommand(Guid bookId, string userName)
    {
        BookId = bookId;
        UserName = userName;
    }
}

public class RentBookCommandHandler : ICommandHandler<RentBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public RentBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(RentBookCommand command, CancellationToken cancellationToken = default)
    {
        var book =
            await _bookRepository.GetBookAsync(command.BookId, cancellationToken)
            ?? throw new BookWasntFoundException(command.BookId);

        var rentedBook = book.Rent(command.UserName);
        await _bookRepository.UpdateStatusAsync(
            rentedBook,
            command.UserName,
            null,
            cancellationToken
        );
    }
}
