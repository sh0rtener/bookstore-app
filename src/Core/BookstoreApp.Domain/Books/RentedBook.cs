using BookstoreApp.Domain.Books.Exceptions;
using BookstoreApp.Domain.Books.Validations;

namespace BookstoreApp.Domain.Books;

public class RentedBook : Book
{
    public string UserName { get; private set; }

    public RentedBook(
        string title,
        string description,
        int pagesCount,
        string author,
        string userName
    )
        : base(title, description, pagesCount, author)
    {
        var usernameSpecification = new IsSatisfiedUsername();

        if (!usernameSpecification.IsSatisfiedBy(userName))
            throw new InvalidUsernameException();

        UserName = userName;
    }

    public override BookedBook Booking(string userName, DateTime date) =>
        throw new BookAlreadyRentException(Id);

    public override RentedBook Rent(string userName) => throw new BookAlreadyHasStatusException();
}
