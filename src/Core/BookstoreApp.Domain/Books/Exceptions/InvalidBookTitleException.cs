using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class InvalidBookTitleException : DomainException
{
    public InvalidBookTitleException() : base("Название книги имеет неверный формат")
    {
    }
}