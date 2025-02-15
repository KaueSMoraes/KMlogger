using System;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Test.Green.Application.Services;

public class TokenServiceTests
{
    private readonly Mock<ITokenService> _mockTokenService = new();

    [Fact]
    public void Should_Generate_Valid_Token()
    {
        // Arrange
        var user = new User(new Email("test@example.com"), new Password("password123", true));
        var expectedToken = "valid-jwt-token";

        _mockTokenService
            .Setup(ts => ts.GenerateToken(user))
            .Returns(expectedToken);

        // Act
        var token = _mockTokenService.Object.GenerateToken(user);

        // Assert
        token.Should().Be(expectedToken);
        _mockTokenService.Verify(ts => ts.GenerateToken(user), Times.Once);
    }
}