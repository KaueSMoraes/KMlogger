using System;
using AutoMapper;
using Bogus;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.ValueObjects;
using Moq;

using RegisterMapper = Application.UseCases.User.Register.Mapper;
using RegisterRequest = Application.UseCases.User.Register.Request;
using RegisterHandler = Application.UseCases.User.Register.Handler;
using FluentAssertions;

namespace Test.Red.Application.UseCases;

public class RegisterHandlerRedTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IDbCommit> _mockDbCommit;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly IMapper _mapper; 
    private readonly Faker _faker;

    public RegisterHandlerRedTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockDbCommit = new Mock<IDbCommit>();
        _mockEmailService = new Mock<IEmailService>();

        // Configurar o AutoMapper
        var config = new MapperConfiguration(cfg => cfg.AddProfile<RegisterMapper>());
        _mapper = config.CreateMapper();

        _faker = new Faker();
    }

    [Fact]
    public async Task Should_Return_404_When_User_Validation_Fails()
    {
        // Arrange
        var registerRequest = new RegisterRequest(
            "invalidemail",
            "",
            "",
            "",
            null,
            null,
            null,
            null,
            "123"
        );

        var invalidUser = new User(
            fullName: new FullName(registerRequest.FirstName, registerRequest.LastName),
            email: new Email(registerRequest.Email),
            address: new Address(registerRequest.Number, registerRequest.NeighBordHood, registerRequest.Road, registerRequest.Complement),
            active: false,
            password: new Password(registerRequest.Password)
        );

        _mockUserRepository
            .Setup(repo => repo.GetByEmail(registerRequest.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<User>());

        _mockUserRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new RegisterHandler(_mockUserRepository.Object, _mockDbCommit.Object, _mapper, _mockEmailService.Object);

        // Act
        var response = await handler.Handle(registerRequest, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.statuscode.Should().Be(404);

        _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockDbCommit.Verify(commit => commit.Commit(It.IsAny<CancellationToken>()), Times.Never);
        _mockEmailService.Verify(service => service.SendEmailAsync(
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
