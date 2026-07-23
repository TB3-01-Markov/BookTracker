using BookTracker.Api.Application.CreateBook;
using BookTracker.Api.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace BookTracker.Api.Tests.IntegrationTests.CreateBook;

public class CreateBookTests : IntegrationTest
{
    //private readonly WebApplicationFactory<Program> factory = new();
    //private readonly CustomWebApplicationFactory factory = new();


    [Fact]
    public async Task PostBookCreatesBook()
    {
        var request =
            new CreateBookRequest
            {
                Title = "The Heart Is a Lonely Hunter",
                Author = "Carson McCullers",
                Year = 1940
            };
        //var client = factory.CreateClient();
        //var response = await client.PostAsJsonAsync("/books", request);
        var response = await Client.PostAsJsonAsync("/books", request);
        var created = await response.Content.ReadFromJsonAsync<CreateBookResponse>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal("The Heart Is a Lonely Hunter", created.Title);

        //................................................................

        //var reader = factory.GetReader();
        //var book = reader.Query(context => context.Find<Book>(created.Id));
        var book = Reader.Query(context => context.Find<Book>(created.Id));

        Assert.NotNull(book);
        Assert.Equal("The Heart Is a Lonely Hunter", book.Title.Value);
        Assert.Equal("Carson McCullers", book.Author.Value);
        Assert.Equal(1940, book.Year);
    }

    [Fact]
    public async Task PostBookReturnsBadRequestWhenTitleIsWhitespace()
    {
        var request =
            new CreateBookRequest
            {
                Title = "   ",
                Author = "Carson McCullers",
                Year = 1940
            };

        var response = await Client.PostAsJsonAsync("/books", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}