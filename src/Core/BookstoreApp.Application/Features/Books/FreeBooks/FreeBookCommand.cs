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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;

    public FreeBookCommandHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
    }

    public async Task Handle(FreeBookCommand command, CancellationToken cancellationToken = default)
    {
        var book =
            await _bookRepository.GetBookAsync(command.BookId, cancellationToken)
            ?? throw new BookWasntFoundException(command.BookId);

        var bookedBook = book.Free();
        _unitOfWork.Begin();
        await _bookRepository.UpdateStatusAsync(bookedBook, null, null, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
