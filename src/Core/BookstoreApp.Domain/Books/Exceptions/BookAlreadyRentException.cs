using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class BookAlreadyRentException : DomainException
{
    public BookAlreadyRentException(Guid bookId)
        : base(string.Format("Книга с идентификатором {0} уже арендована", bookId)) { }
}
