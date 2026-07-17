using BookTracker.Api.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.PortableExecutable;

namespace BookTracker.Api.Tests.IntegrationTests;

public class EfReader(IServiceProvider services)
{
    public T Query<T>(Func<AppDbContext, T> query)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //var count = reader.Query(db => db.Books.Count());
        //var book = reader.Query(db =>db.Books.Single(book => book.Title == "Dune"));
        return query(db);
    }
}