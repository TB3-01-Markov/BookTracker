using BookTracker.Api.Application.BookList;
using BookTracker.Api.Application.CreateBook;
using BookTracker.Api.Domain;
using BookTracker.Api.Storage;

namespace BookTracker.Api.Application;

public class BookService(IBookRepository bookRepository)
{
    public async Task<IReadOnlyList<BookInfo>> GetAllBooks()
    {
        var books = await bookRepository.GetAllAsync();
        var summary = books.Select(book => new BookInfo
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author
        });
        // Gebruik LINQ om de entiteiten in `books` te mappen naar een `BookInfo` lijst. 
        // return [.. summary];
        return summary.ToList();
    }

    public async Task<CreateBookResponse> CreateBook(CreateBookRequest request)
    {
        var book = new Book{
            // map de velden van request naar de properties van dit nieuwe object
            Title = request.Title,
            Author = request.Author,
            Year = request.Year
        };
        var savedBook = await bookRepository.AddAsync(book);
        return new CreateBookResponse
            {
            // map de velden van de `savedBook` entiteit naar de properties van de response DTO
            Id = savedBook.Id,
            Title = savedBook.Title,
            Author = savedBook.Author,
            Year = savedBook.Year
        };
    }
}
