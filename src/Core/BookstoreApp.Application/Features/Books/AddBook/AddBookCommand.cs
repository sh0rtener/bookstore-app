using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Application.Features.Books.AddBook;

public class AddBookCommand : ICommand
{
    public AddBookCommandIn Input { get; set; }

    public AddBookCommand(AddBookCommandIn input) => Input = input;
}

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public AddBookCommandHandler(IBookRepository bookRepository)
    {
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

        await _bookRepository.AddBookAsync(book, cancellationToken);
    }
}
