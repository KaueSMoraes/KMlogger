using System;
using Domain.Entities;
using Domain.ValueObjects;

namespace Test.Red.Domain.Entities;

public class UserTests
{
    [Fact]
    public void User_Should_Be_Invalid_When_Properties_Are_Invalid()
    {
        // Arrange
        var fullName = new FullName("", ""); // Nome inválido
        var email = new Email("invalid-email"); // Email inválido
        var address = new Address(-1, "", "", ""); // Endereço inválido
        var user = new User(fullName, email, address, true, new Password("teste"));

        // Act & Assert
        Assert.False(user.IsValid); // O objeto deve ser inválido
        Assert.Contains(user.Notifications, n => n.Message == "First name cannot be null or empty");
        Assert.Contains(user.Notifications, n => n.Message == "Email invalid");
        Assert.Contains(user.Notifications, n => n.Message == "Number must be greater than 0");
    }
}