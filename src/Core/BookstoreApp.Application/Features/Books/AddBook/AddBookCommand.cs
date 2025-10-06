using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Application.Features.Books.AddBook;

public class AddBookCommand : ICommand
{
    public required AddBookCommandIn Input { get; set; }

    public AddBookCommand(AddBookCommandIn input) => Input = input;
}

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;

    public AddBookCommandHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
    }

    public async Task Handle(AddBookCommand command, CancellationToken cancellationToken = default)
    {
        var book = new FreeBook(
            command.Input.Title ?? "",
            command.Input.Description ?? "",
            command.Input.PagesCount,
            command.Input.Author ?? ""
        );

        _unitOfWork.Begin();
        await _bookRepository.AddBookAsync(book, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
