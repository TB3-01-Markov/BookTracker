/*
//using BookTracker.Api.Application.BookList;
using BookTracker.Api.Application.CreateBook;
//using BookTracker.Api.Application.GetBookById;
using BookTracker.Api.Application.UpdateBook;
using BookTracker.Api.Domain;
using BookTracker.Api.Storage;

namespace BookTracker.Api.Application;

public class BookService(IBookRepository bookRepository)
{
    public async Task<CreateBookResponse> CreateBook(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = new BookTitle(request.Title),
            Author = new AuthorName(request.Author),
            Year = request.Year
        };

        var savedBook = await bookRepository.AddAsync(book);

        return new CreateBookResponse
        {
            Id = savedBook.Id,
            Title = savedBook.Title.Value,
            Author = savedBook.Author.Value,
            Year = savedBook.Year
        };
    }
    public async Task<bool> DeleteBook(int id)
    {
        //var BookRepository = new InMemoryBookRepository();
        return await bookRepository.DeleteAsync(id);
    }


    public async Task<bool> UpdateBook(int id, UpdateBookRequest request)
    {
        var book =
            new Book
            {
                Id = id,
                Title = new BookTitle(request.Title),// ... create value object here,
                Author = new AuthorName(request.Author), // ... create value object here,
                Year = request.Year
            };

        return await bookRepository.UpdateAsync(book);

    }
}*/
    /*
public async Task<bool> UpdateBook(int id, UpdateBookRequest request)
{
    var book =
        new Book
        {
            Id = id,
            Title = request.Title,
            Author = request.Author,
            Year = request.Year
        };

    return await bookRepository.UpdateAsync(book);
}
*/
    /*
    public async Task<IReadOnlyList<BookInfo>> GetAllBooks()
    {
        var books = await bookRepository.GetAllAsync();
        var summary = books.Select(book => new BookInfo
        {
            Id = book.Id,
            Title = book.Title.Value,
            Author = book.Author.Value
        });
        // Gebruik LINQ om de entiteiten in `books` te mappen naar een `BookInfo` lijst. 
        // return [.. summary];
        return summary.ToList();
    }
    */
    /*
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
    */

    /*
    public async Task<BookDetails?> GetBookById(int id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return null;
        }

        return
            new BookDetails
            {
                Id = book.Id,
                Title = book.Title.Value,
                Author = book.Author.Value,
                Year = book.Year
            };
    }
    */
