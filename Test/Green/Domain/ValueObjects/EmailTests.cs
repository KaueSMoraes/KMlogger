using System;
using Domain.ValueObjects;

namespace Test.Green.Domain.ValueObjects;


public class EmailTests
{
    [Fact]
    public void Email_Should_Be_Valid_When_Valid_Email_Provided()
    {
        var email = new Email("test@example.com");
        Assert.True(email.IsValid); 
    }
}