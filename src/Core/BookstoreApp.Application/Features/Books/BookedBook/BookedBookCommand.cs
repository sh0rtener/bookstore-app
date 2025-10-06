using BookstoreApp.Application.Features.Books.Exceptions;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Application.Features.Books.BookedBook;

public class BookedBookCommand : ICommand
{
    public Guid BookId { get; set; }
    public string UserName { get; set; }
    public DateTime BookingExpires { get; set; }

    public BookedBookCommand(Guid bookId, string userName, DateTime bookingExpires)
    {
        BookId = bookId;
        UserName = userName;
        BookingExpires = bookingExpires;
    }
}

public class BookedBookCommandHandler : ICommandHandler<BookedBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public BookedBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(
        BookedBookCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var book =
            await _bookRepository.GetBookAsync(command.BookId, cancellationToken)
            ?? throw new BookWasntFoundException(command.BookId);

        var bookedBook = book.Booking(command.UserName, command.BookingExpires);
        await _bookRepository.UpdateStatusAsync(
            bookedBook,
            command.UserName,
            command.BookingExpires,
            cancellationToken
        );
    }
}
