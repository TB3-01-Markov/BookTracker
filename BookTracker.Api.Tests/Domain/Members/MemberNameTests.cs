using BookTracker.Api.Domain;
using BookTracker.Api.Domain.Books;
using BookTracker.Api.Domain.Members;

namespace BookTracker.Api.Tests.Domain.Members;

public class MemberNameTests
{
    [Fact]
    public void MemberNameAcceptsValidName()
    {
        var member = new MemberName("F. Scott Fitzgerald");

        Assert.Equal("F. Scott Fitzgerald", member.Value);
    }

    [Fact]
    public void MemberNameTrimsValue()
    {
        var member = new MemberName("  Frank Herbert  ");
        Assert.Equal("Frank Herbert", member.Value);

    }

    [Fact]
    public void MemberNameRejectsWhitespace()
    {
        var exception = Assert.Throws<DomainException>(() => new MemberName("   "));
        Assert.Equal("Member is required.", exception.Message);
    }

    [Fact]
    public void MemberNameRejectsNameLongerThan100Characters()
    {
        var tooLong = new string('x', 101);

        var exception = Assert.Throws<DomainException>(() => new MemberName(tooLong));

        Assert.Equal("Member cannot be longer than 100 characters.", exception.Message);
    }
}
