using BookTracker.Api.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BookTracker.Api.Tests.IntegrationTests;

public class EfWriter(IServiceProvider services)
{
    public void Seed(Action<AppDbContext> seed)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        seed(db);
        //writer.Seed(db =>{db.Books.Add(new Book{Title = "Dune",Author = "Frank Herbert",Year = 1965});});
        db.SaveChanges();
    }
}