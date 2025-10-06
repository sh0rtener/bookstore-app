using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books.Exceptions;

public class InvalidBookPagesCountException : DomainException
{
    public InvalidBookPagesCountException()
        : base("Задано некорректное количество страниц для книги") { }
}
