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

namespace Test.Green.Application.UseCases
{
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
        public async Task Handle_ShouldReturnSuccess_WhenUserExists()
        {
            // Arrange
            var fakeEmail = new Email(_faker.Internet.Email());
            var fakePassword = new Password(_faker.Internet.Password());
            var fakeFullName = new FullName(_faker.Person.FirstName, _faker.Person.LastName);
            var fakeAddress = new Address(
                long.Parse(_faker.Address.StreetAddress()),
                _faker.Address.City(),
                _faker.Address.ZipCode(),
                _faker.Address.State()
            );

            var fakeUser = new User(fakeFullName, fakeEmail, fakeAddress, true, fakePassword);

            var request = new Request(fakeUser.Email.Address!, fakeUser.TokenActivate);

            _userRepositoryMock
                .Setup(repo => repo.GetByEmail(request.email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeUser);

            _userRepositoryMock.Setup(repo => repo.Update(It.IsAny<User>()));
            _dbCommitMock.Setup(db => db.Commit(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _emailServiceMock.Setup(email => email.SendEmailAsync(
                fakeUser.FullName.FirstName,
                fakeUser.Email.Address!,
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())
            ).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.statuscode.Should().Be(200);
            result.message.Should().Be("Code sent successfully");

            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
            _dbCommitMock.Verify(db => db.Commit(It.IsAny<CancellationToken>()), Times.Once);
            _emailServiceMock.Verify(email => email.SendEmailAsync(
                fakeUser.FullName.FirstName,
                fakeUser.Email.Address!,
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
