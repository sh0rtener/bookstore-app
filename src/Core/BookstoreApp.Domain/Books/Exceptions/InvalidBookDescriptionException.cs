using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class InvalidBookDescriptionException : DomainException
{
    public InvalidBookDescriptionException() : base("Название книги имеет неверный формат")
    {
    }
}