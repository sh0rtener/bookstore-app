namespace BookstoreApp.Application.Features.Books.AddBook;

public class AddBookCommandIn
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public int PagesCount { get; set; }
}
