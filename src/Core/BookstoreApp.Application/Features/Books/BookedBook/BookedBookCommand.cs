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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;

    public BookedBookCommandHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
    {
        _unitOfWork = unitOfWork;
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
        _unitOfWork.Begin();
        await _bookRepository.UpdateStatusAsync(
            bookedBook,
            command.UserName,
            command.BookingExpires,
            cancellationToken
        );
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
