using BookstoreApp.Application.Features.Books.Exceptions;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Application.Features.Books.RemoveBook;

public class RemoveBookCommand : ICommand
{
    public Guid Id { get; set; }

    public RemoveBookCommand(Guid id)
    {
        Id = id;
    }
}

public class RemoveBookCommandHandler : ICommandHandler<RemoveBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;

    public RemoveBookCommandHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
    }

    public async Task Handle(RemoveBookCommand command, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetBookAsync(command.Id, cancellationToken)
            ?? throw new BookWasntFoundException(command.Id);

        _unitOfWork.Begin();
        await _bookRepository.RemoveBookAsync(command.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);          
    }
}
