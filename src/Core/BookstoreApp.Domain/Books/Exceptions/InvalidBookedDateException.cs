using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class InvalidBookedDateException : DomainException
{
    public InvalidBookedDateException()
        : base("Нельзя забронировать книгу более чем на определенный период") { }
}
