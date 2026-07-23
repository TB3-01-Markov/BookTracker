using BookTracker.Api.Storage;

namespace BookTracker.Api.Application.BookList;

public class GetBookListQuery(IBookRepository bookRepository)
{
    public async Task<IReadOnlyList<BookInfo>> Execute()//GetAllBooks()
    {// plaats hier de code die nu nog in BookService.GetAllBooks staat
        var books = await bookRepository.GetAllAsync();
        var summary = books.Select(book => new BookInfo
        {
            Id = book.Id,
            Title = book.Title.Value,
            Author = book.Author.Value
        });
        return summary.ToList();
    }
}