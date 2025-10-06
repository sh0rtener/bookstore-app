using BookstoreApp.Domain.Books.Exceptions;

namespace BookstoreApp.Domain.Books;

public class FreeBook : Book
{
    public FreeBook(string title, string description, int pagesCount, string author)
        : base(title, description, pagesCount, author) { }

    public override BookedBook Booking(string userName, DateTime date)
    {
        return new(Title, Description, PagesCount, Author, userName, date) { Id = Id };
    }

    public override FreeBook Free() => throw new BookAlreadyHasStatusException();

    public override RentedBook Rent(string userName)
    {
        return new(Title, Description, PagesCount, Author, userName) { Id = Id };
    }
}
