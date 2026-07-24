using BookTracker.Api.Application;
using BookTracker.Api.Application.BookList;
using BookTracker.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace BookTracker.Api.Tests.IntegrationTests.BookList;

public class BookListTests : IntegrationTest
{
    //private readonly WebApplicationFactory<Program> factory = new();
    //private readonly CustomWebApplicationFactory factory = new();
    
    [Fact]
    public async Task GetBooksReturnsBooks()
    {
      //  var writer = factory.GetWriter();
        Writer.Seed(db => db.Books.Add(
            new Book
            {
                Title = new BookTitle("Cannery Row"),
                Author = new AuthorName("John Steinbeck"),
                Year = 1945
            }
        ));

       // var client = factory.CreateClient();

        var response = await Client.GetAsync("/books");
        //var books = await response.Content.ReadFromJsonAsync<List<BookInfo>>();
       // var book = Reader.Query(context => context.Find<Book>(created.Id));
       // var result = await Client.GetFromJsonAsync<PagedResult<BookInfo>>("/books");
        var result = await response.ReadJsonAs<PagedResult<BookInfo>>(HttpStatusCode.OK);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //Assert.NotNull(books);

        //var bookInfo = Assert.Single(books);
        //Assert.Equal("Cannery Row", bookInfo.Title);
        //Assert.Equal("John Steinbeck", bookInfo.Author);

        Assert.NotNull(result);

        var bookInfo = Assert.Single(result.Items);

        Assert.Equal("Cannery Row", bookInfo.Title);
        Assert.Equal("John Steinbeck", bookInfo.Author);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(1, result.TotalItems);
        Assert.Equal(1, result.TotalPages);
    }

    [Fact]
    public async Task GetBooksReturnsRequestedPage()
    {
        //var writer = factory.GetWriter();
        Writer.Seed(db =>
        {
            db.Books.AddRange(
                new Book
                {
                    Title = new BookTitle("Book 1"),
                    Author = new AuthorName("Author 1"),
                    Year = 2001
                },
                new Book
                {
                    Title = new BookTitle("Book 2"),
                    Author = new AuthorName("Author 2"),
                    Year = 2002
                },
                new Book
                {
                    Title = new BookTitle("Book 3"),
                    Author = new AuthorName("Author 3"),
                    Year = 2003
                });
        });

        // var client = factory.CreateClient();
        var response = await Client.GetAsync("/books?page=2&pageSize=1");
        var result = await response.ReadJsonAs<PagedResult<BookInfo>>(HttpStatusCode.OK);
        //var result = await Client.GetFromJsonAsync<PagedResult<BookInfo>>("/books?page=2&pageSize=1");

        Assert.NotNull(result);

        var book = Assert.Single(result.Items);

        Assert.Equal("Book 2", book.Title);
        Assert.Equal(2, result.Page);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(3, result.TotalItems);
        Assert.Equal(3, result.TotalPages);
    }

    [Fact]
    public async Task GetBooksReturnsEmptyItemsWhenPageIsTooHigh()
    {
        //var writer = factory.GetWriter();
        Writer.Seed(db =>
        {
            db.Books.Add(
                new Book
                {
                    Title = new BookTitle("Book 1"),
                    Author = new AuthorName("Author 1"),
                    Year = 2001
                });
        });

        //var client = factory.CreateClient();
        var response = await Client.GetAsync("/books?page=99&pageSize=10");
        var result = await response.ReadJsonAs<PagedResult<BookInfo>>(HttpStatusCode.OK);
        //var result = await Client.GetFromJsonAsync<PagedResult<BookInfo>>("/books?page=99&pageSize=10");

        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(99, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(1, result.TotalItems);
        Assert.Equal(1, result.TotalPages);
    }
}