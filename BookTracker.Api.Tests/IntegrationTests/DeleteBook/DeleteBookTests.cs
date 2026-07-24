using System.Net;
using BookTracker.Api.Domain;

namespace BookTracker.Api.Tests.IntegrationTests.DeleteBook;

public class DeleteBookTests: IntegrationTest
{
    //private readonly CustomWebApplicationFactory factory = new();

    [Fact]
    public async Task DeleteBookRemovesBook()
    {
      //  var writer = factory.GetWriter();

        Writer.Seed(db =>
        {
            db.Books.Add(
                new Book
                {   
                    Title = new BookTitle("Dune"),
                    Author = new AuthorName("Frank Herbert"),
                    Year = 1965
                });
        });

        //var client = factory.CreateClient();

        var response = await Client.DeleteAsync("/books/1");
        await response.ShouldHaveStatusCode(HttpStatusCode.NoContent);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        //var reader = factory.GetReader();
        var book = Reader.Query(db => db.Books.Find(1));

        Assert.Null(book);
    }

    [Fact]
    public async Task DeleteBookReturnsNotFoundWhenBookDoesNotExist()
    {
        //var client = factory.CreateClient();

        var response = await Client.DeleteAsync("/books/9999");
        await response.ShouldHaveStatusCode(HttpStatusCode.NotFound);

        // voeg hier een assert toe die verifiëert dat status code NotFound is.

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}