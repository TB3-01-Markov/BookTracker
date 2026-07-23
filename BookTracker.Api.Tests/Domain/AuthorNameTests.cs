using BookTracker.Api.Domain;

namespace BookTracker.Api.Tests.Domain;

public class AuthorNameTests
{
    [Fact]
    public void AuthorNameAcceptsValidName()
    {
        var author = new AuthorName("F. Scott Fitzgerald");

        Assert.Equal("F. Scott Fitzgerald", author.Value);
    }

    [Fact]
    public void AuthorNameTrimsValue()
    {
        // Implementeer hier deze test
        var author = new AuthorName("  Frank Herbert  ");
        Assert.Equal("Frank Herbert", author.Value);

    }

    // Voeg hier de test 'AuthorNameRejectsWhitespace' toe
    // exception.Message = "Author is required."
    public void AuthorNameRejectsWhitespace()
    {
        var exception = Assert.Throws<DomainException>(() => new AuthorName("   "));
        Assert.Equal("Author is required.", exception.Message);
    }

    // Voeg hier de test 'AuthorNameRejectsNameLongerThan100Characters' toe
    // exception.Message = "Author cannot be longer than 100 characters."
    public void AuthorNameRejectsNameLongerThan100Characters()
    {
        var tooLong = new string('x', 101);

        var exception = Assert.Throws<DomainException>(() => new AuthorName(tooLong));

        Assert.Equal("Author Name cannot be longer than 100 characters.", exception.Message);
    }
}