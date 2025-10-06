namespace BookstoreApp.Application.Features.Books.Exceptions;

public class BookWasntFoundException : Common.ApplicationException
{
    public BookWasntFoundException(Guid id)
        : base(string.Format("Книга с идентификатором {0} не найдена!", id)) { }
}
