using System.Net;
using System.Net.Http.Json;
using BookTracker.Api.Application.UpdateBook;
using BookTracker.Api.Domain;

namespace BookTracker.Api.Tests.IntegrationTests.UpdateBook;

public class UpdateBookTests: IntegrationTest
{
    //private readonly CustomWebApplicationFactory factory = new();

    [Fact]
    public async Task PutBookUpdatesBook()
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

        var request =
            new UpdateBookRequest
            {
                Title = "Dune Messiah",
                Author = "Frank Herbert",
                Year = 1969
            };

        //var client = factory.CreateClient();

        var response = await Client.PutAsJsonAsync("/books/1", request);
        await response.ShouldHaveStatusCode(HttpStatusCode.NoContent);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        //var reader = factory.GetReader();
        var book = Reader.Query(db => db.Books.Find(1));

        Assert.NotNull(book);
        // voeg hier de Asserts toe die de properties van book checken
        // gebruik de literal waarden voor de 'expected' values, bvb 1969, niet request.Year
        Assert.Equal(1, book.Id);
        Assert.Equal("Dune Messiah", book.Title.Value);
        Assert.Equal("Frank Herbert", book.Author.Value);
        Assert.Equal(1969, book.Year);
    }

    [Fact]
    public async Task PutBookReturnsNotFoundWhenBookDoesNotExist()
    {
        var request =
            new UpdateBookRequest
            {
                Title = "Unknown Book",
                Author = "Unknown Author",
                Year = 2000
            };

        //var client = factory.CreateClient();

        var response = await Client.PutAsJsonAsync("/books/9999", request);
        await response.ShouldHaveStatusCode(HttpStatusCode.NotFound);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}