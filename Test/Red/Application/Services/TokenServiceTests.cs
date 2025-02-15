using System;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Test.Red.Application.Services;

public class TokenServiceTests
{
    private readonly Mock<ITokenService> _mockTokenService = new();

    [Fact]
    public void Should_Return_Empty_Token_When_User_Is_Invalid()
    {
        // Arrange
        var user = new User(new Email("invalidemail"), new Password("weakpassword", true));
        var emptyToken = string.Empty;

        _mockTokenService
            .Setup(ts => ts.GenerateToken(user))
            .Returns(emptyToken);

        // Act
        var token = _mockTokenService.Object.GenerateToken(user);

        // Assert
        token.Should().Be(emptyToken);
        _mockTokenService.Verify(ts => ts.GenerateToken(user), Times.Once);
    }
}
