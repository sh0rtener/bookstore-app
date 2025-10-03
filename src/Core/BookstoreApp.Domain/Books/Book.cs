using BookstoreApp.Domain.Books.Exceptions;
using BookstoreApp.Domain.Books.Validations;
using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books;

public abstract class Book : Entity<Guid>
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Description { get; private set; }
    public int PagesCount { get; private set; }
    public BookStatus Status { get; protected set; } = BookStatus.Free;

    public Book(string title, string description, int pagesCount, string author)
    {
        var titleSpecification = new IsSatisfiedBookTitle();
        var descriptionSpecification = new IsSatisfiedBookDescription();
        var pagesCountSpecification = new IsSatisfiedBookPagesCount();
        var authorSpecification = new IsSatisfiedBookAuthor();

        if (!titleSpecification.IsSatisfiedBy(title))
            throw new InvalidBookTitleException();

        if (!descriptionSpecification.IsSatisfiedBy(description))
            throw new InvalidBookDescriptionException();

        if (!pagesCountSpecification.IsSatisfiedBy(pagesCount))
            throw new InvalidBookPagesCountException();

        if (!authorSpecification.IsSatisfiedBy(author))
            throw new InvalidBookAuthorException();

        Title = title;
        Description = description;
        PagesCount = pagesCount;
        Author = author;
    }

    public abstract RentedBook Rent(string userName);
    public virtual FreeBook Free() 
    {
        return new FreeBook(Title, Description, PagesCount, Author) { Id = Id };
    }
    public abstract BookedBook Booking(string userName, DateTime date);
}
