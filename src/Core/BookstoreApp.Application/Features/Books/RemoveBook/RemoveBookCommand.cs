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
    private readonly IBookRepository _bookRepository;

    public RemoveBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(RemoveBookCommand command, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetBookAsync(command.Id, cancellationToken)
            ?? throw new BookWasntFoundException(command.Id);

        await _bookRepository.RemoveBookAsync(command.Id, cancellationToken);     
    }
}
