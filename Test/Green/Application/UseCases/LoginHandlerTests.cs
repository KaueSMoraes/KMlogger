using System;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;

using LoginRequest = Application.UseCases.User.Login.Request;
using LoginHandler = Application.UseCases.User.Login.Handler;
using FluentAssertions;
using Domain.ValueObjects;
using Domain.Entities;

namespace Test.Green.Application.UseCases;

public class LoginHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository = new();
    private readonly Mock<ITokenService> _mockTokenService = new();
    private readonly Mock<IMapper> _mockMapper = new();

    [Fact]
    public async Task Should_Authenticate_User_And_Return_Token()
    {
        // Arrange
        var request = new LoginRequest("test@example.com", "password123");
        var user = new User(new Email(request.email), new Password(request.password, true));
        var token = "generated-token";

        _mockMapper.Setup(m => m.Map<User>(request)).Returns(user);
        _mockUserRepository.Setup(r => r.Authenticate(user, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockTokenService.Setup(t => t.GenerateToken(user)).Returns(token);

        var handler = new LoginHandler(_mockUserRepository.Object, _mockTokenService.Object, _mockMapper.Object);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        response.statuscode.Should().Be(200);
        response.Token.Should().Be(token);
        response.notifications.Should().BeNullOrEmpty();
    }
}