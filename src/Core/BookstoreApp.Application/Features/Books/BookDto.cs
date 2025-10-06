namespace BookstoreApp.Application.Features.Books;

public class BookDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public int PagesCount { get; set; }
    public string Status { get; set; } = "Free";
}
