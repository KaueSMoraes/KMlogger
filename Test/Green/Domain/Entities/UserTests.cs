using System;
using Domain.Entities;
using Domain.ValueObjects;

namespace Test.Green.Domain.Entities;

public class UserTests
{
    [Fact]
    public void User_Should_Be_Valid_When_Valid_Properties_Are_Provided()
    {
        // Arrange
        var fullName = new FullName("John", "Doe");
        var email = new Email("john.doe@example.com");
        var address = new Address(123, "Downtown", "Main St", "Apt 101");
        var password = new Password("testessssssss");
        var user = new User(fullName, email, address, true, password);
        Assert.True(user.IsValid); 
    }
}