namespace BookTracker.Api.Domain;
/*
public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public int Year { get; set; }
}
*/
public class Book
{
    public int Id { get; set; }
    public required BookTitle Title { get; set; }
    public required AuthorName Author { get; set; }
    public int Year { get; set; }
}
