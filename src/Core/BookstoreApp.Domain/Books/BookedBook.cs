using BookstoreApp.Domain.Books.Exceptions;
using BookstoreApp.Domain.Books.Validations;
using BookstoreApp.Domain.Common.Specifications;

namespace BookstoreApp.Domain.Books;

public class BookedBook : Book
{
    public string UserName { get; private set; }
    public DateTime BookedTo { get; private set; }

    public BookedBook(
        string title,
        string description,
        int pagesCount,
        string author,
        string userName,
        DateTime bookedTo
    )
        : base(title, description, pagesCount, author)
    {
        var usernameSpecification = new IsSatisfiedUsername();
        var bookedToSpecification = new IsDatetimeMoreThanYesterday().And(
            new IsSatisfiedBookedDate()
        );

        if (!usernameSpecification.IsSatisfiedBy(userName))
            throw new InvalidUsernameException();

        if (!bookedToSpecification.IsSatisfiedBy(bookedTo))
            throw new InvalidBookedDateException();

        UserName = userName;
        BookedTo = bookedTo;
    }

    public override BookedBook Booking(string userName, DateTime date) =>
        throw new BookAlreadyHasStatusException();

    public override RentedBook Rent(string userName) => throw new BookAlreadyBookedException(Id);
}
