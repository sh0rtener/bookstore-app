using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class BookAlreadyHasStatusException : DomainException
{
    public BookAlreadyHasStatusException()
        : base("Книга уже имеет идентичный статус. Долбаеб..") { }
}
