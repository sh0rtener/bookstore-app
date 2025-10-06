using BookstoreApp.Domain.Common;

namespace BookstoreApp.Domain.Books;

public class BookStatus : ValueObject
{
    private string _name;
    public string Name => _name;
    public static BookStatus Rent => new("Rent");
    public static BookStatus Booked => new("Booked");
    public static BookStatus Free => new("Free");

    protected BookStatus(string name) => _name = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
