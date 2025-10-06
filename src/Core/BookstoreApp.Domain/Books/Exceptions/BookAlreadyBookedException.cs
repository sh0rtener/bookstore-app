using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class BookAlreadyBookedException : DomainException
{
    public BookAlreadyBookedException(Guid bookId)
        : base(string.Format("Книга с идентификатором {0} уже зарезервирована", bookId)) { }
}
