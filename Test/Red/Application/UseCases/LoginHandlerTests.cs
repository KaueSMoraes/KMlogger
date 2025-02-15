using System;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;

using LoginRequest = Application.UseCases.User.Login.Request;
using LoginHandler = Application.UseCases.User.Login.Handler;
using FluentAssertions;
using Domain.ValueObjects;

namespace Test.Red.Application.UseCases;

public class LoginHandlerRedTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IMapper> _mockMapper;

    public LoginHandlerRedTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenService = new Mock<ITokenService>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Should_Return_403_When_Authentication_Fails()
    {
        // Arrange
        var request = new LoginRequest("test@example.com", "wrongpassword");
        var user = new User(new Email(request.email), new Password(request.password, true));

        _mockMapper.Setup(m => m.Map<User>(request)).Returns(user);
        _mockUserRepository.Setup(r => r.Authenticate(user, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var handler = new LoginHandler(_mockUserRepository.Object, _mockTokenService.Object, _mockMapper.Object);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        response.statuscode.Should().Be(403);
        response.Token.Should().BeNullOrEmpty();
    }
}