namespace BookstoreApp.Persistense.Daos;

public class BookDao
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public int PagesCount { get; set; }
    public string Status { get; set; } = "Free";
    public string? UserName { get; set; } 
    public DateTime? BookedTo { get; set;   }
}
