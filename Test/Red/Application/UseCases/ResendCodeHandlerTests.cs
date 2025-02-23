using System;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.User.ResendCode;
using Bogus;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Test.Red.Application.UseCases;

public class ResendCodeHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<IDbCommit> _dbCommitMock;
    private readonly Handler _handler;
    private readonly Faker _faker;

    public ResendCodeHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _dbCommitMock = new Mock<IDbCommit>();
        _handler = new Handler(_userRepositoryMock.Object, _dbCommitMock.Object, _emailServiceMock.Object);
        _faker = new Faker();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var fakeEmail = _faker.Internet.Email();
        var request = new Request(fakeEmail, 1234);

        _userRepositoryMock
            .Setup(repo => repo.GetByEmail(request.email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null); 
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.statuscode.Should().Be(404);
        result.message.Should().Be("Request invalid");

        _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
        _dbCommitMock.Verify(db => db.Commit(It.IsAny<CancellationToken>()), Times.Never);
        _emailServiceMock.Verify(email => email.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()
        ), Times.Never);
    }
}
