using BookTracker.Api.Domain;
using BookTracker.Api.Domain.Books;
using BookTracker.Api.Domain.Members;

namespace BookTracker.Api.Tests.Domain.Members;

public class MemberEmailTests
{
    [Fact]
    public void MemberEmailAcceptsValidEmail()
    {
        var mail = new MemberEmail("membermail@gmail.com");

        Assert.Equal("membermail@gmail.com", mail.Value);
    }
    /*
    [Fact]
    public void MemberEmailSymbolContain()
    {
        var mail1 = new MemberEmail("membermail@gmail.com");
        var mail2 = new MemberEmail("membermailgmail.com");
        Assert.True(mail1.Value.Contains('@'));
        Assert.False(mail2.Value.Contains('@'));
    }
    */
    [Fact]
    public void MemberEmailRejectsValueWithoutAtSymbol(){
        var exception = Assert.Throws<DomainException>(() => new MemberEmail("membermailgmail.com"));
        Assert.Equal("Email must contain the @ symbol", exception.Message);
    }
    [Fact]
    public void MemberEmailTrimsValue()
    {
        var mail = new MemberEmail("  membermail@gmail.com  ");
        Assert.Equal("membermail@gmail.com", mail.Value);

    }

    [Fact]
    public void MemberEmailRejectsWhitespace()
    {
        var exception = Assert.Throws<DomainException>(() => new MemberEmail("   "));
        Assert.Equal("Email is required.", exception.Message);
    }

    [Fact]
    public void MemberEmailRejectsNameLongerThan200Characters()
    {
        var tooLong = new string('x', 201);

        var exception = Assert.Throws<DomainException>(() => new MemberEmail(tooLong));

        Assert.Equal("Email cannot be longer than 200 characters.", exception.Message);
    }
}
