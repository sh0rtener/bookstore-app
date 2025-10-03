using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class InvalidUsernameException : DomainException
{
    public InvalidUsernameException()
        : base("Имя пользователя имеет неверный формат") { }
}
