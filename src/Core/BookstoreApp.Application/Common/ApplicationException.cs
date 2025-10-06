namespace BookstoreApp.Application.Common;

public abstract class ApplicationException : Exception
{
    public ApplicationException(string message)
        : base(message) { }
}
