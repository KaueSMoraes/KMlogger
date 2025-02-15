using System;
using Bogus;
using Domain.ValueObjects;
using FluentAssertions;

namespace Test.Green.Domain.ValueObjects;

public class PasswordTests
{
    private readonly Faker _faker = new(); 
    
    [Fact]
    public void Should_Create_Password_With_Valid_Faker_Input()
    {
        // Arrange
        var password = _faker.Internet.Password(8, true); // Gera uma senha com no mínimo 8 caracteres
        // Act
        var passwordVo = new Password(password);

        // Assert
        passwordVo.Hash.Should().NotBeNullOrEmpty();
        passwordVo.Salt.Should().NotBeNullOrEmpty();
        passwordVo.Content.Should().BeNull();
        passwordVo.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Validate_Password_Correctly_With_Faker_Data()
    {
        // Arrange
        var password = _faker.Internet.Password(10, true); // Senha com pelo menos 10 caracteres
        var passwordVo = new Password(password);

        // Act
        var isValid = passwordVo.VerifyPassword(password, passwordVo.Salt);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Not_Validate_Wrong_Faker_Password()
    {
        // Arrange
        var correctPassword = _faker.Internet.Password(12); // Senha válida
        var wrongPassword = _faker.Internet.Password(12);   // Outra senha
        var passwordVo = new Password(correctPassword);

        // Act
        var isValid = passwordVo.VerifyPassword(wrongPassword, passwordVo.Salt);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Not_Validate_With_Wrong_Salt_Using_Faker()
    {
        // Arrange
        var password = _faker.Internet.Password(10);
        var passwordVo = new Password(password);
        var wrongSalt = _faker.Random.AlphaNumeric(16); // Salt aleatório errado

        // Act
        var isValid = passwordVo.VerifyPassword(password, wrongSalt);

        // Assert
        isValid.Should().BeFalse();
    }
}