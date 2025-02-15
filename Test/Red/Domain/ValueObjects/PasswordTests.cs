using System;
using Bogus;
using Domain.ValueObjects;
using FluentAssertions;

namespace Test.Red.Domain.ValueObjects;

public class PasswordTests
{
    private readonly Faker _faker = new(); 
    [Fact]
    public void Should_Not_Create_Password_With_Null_Input()
    {
        // Arrange
        string password = null;

        // Act
        var passwordVo = new Password(password);

        // Assert
        passwordVo.IsValid.Should().BeFalse();
        passwordVo.Notifications.Should().Contain(n => n.Message == "Password cannot be null or empty");
    }

    [Fact]
    public void Should_Not_Create_Password_With_Empty_Input()
    {
        // Arrange
        var password = "";

        // Act
        var passwordVo = new Password(password);

        // Assert
        passwordVo.IsValid.Should().BeFalse();
        passwordVo.Notifications.Should().Contain(n => n.Message == "Password cannot be null or empty");
    }

    [Fact]
    public void Should_Not_Create_Password_With_Short_Input()
    {
        // Arrange
        var password = "123";

        // Act
        var passwordVo = new Password(password);

        // Assert
        passwordVo.IsValid.Should().BeFalse();
        passwordVo.Notifications.Should().Contain(n => n.Message == "Password must be at least 6 characters long");
    }

    [Fact]
    public void Should_Not_Validate_With_Wrong_Password()
    {
        // Arrange
        var correctPassword = "StrongPassword123";
        var wrongPassword = "WrongPassword456";
        var passwordVo = new Password(correctPassword);

        // Act
        var isValid = passwordVo.VerifyPassword(wrongPassword, passwordVo.Salt);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Not_Validate_With_Wrong_Salt()
    {
        // Arrange
        var password = "StrongPassword123";
        var wrongSalt = "InvalidSalt";
        var passwordVo = new Password(password);

        // Act
        var isValid = passwordVo.VerifyPassword(password, wrongSalt);

        // Assert
        isValid.Should().BeFalse();
    }
}