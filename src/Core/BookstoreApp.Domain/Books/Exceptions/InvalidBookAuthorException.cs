using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class InvalidBookAuthorException : DomainException
{
    public InvalidBookAuthorException() : base("Автор передан в невалидном формате")
    {
    }
}