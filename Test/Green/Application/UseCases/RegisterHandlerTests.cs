using System;
using AutoMapper;
using Bogus;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;

using RegisterMapper = Application.UseCases.User.Register.Mapper;
using RegisterRequest = Application.UseCases.User.Register.Request;
using RegisterHandler = Application.UseCases.User.Register.Handler;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;

namespace Test.Green.Application.UseCases;

public class RegisterHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IDbCommit> _mockDbCommit;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly IMapper _mapper;
    private readonly Faker _faker;

    public RegisterHandlerTests()
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
    public async Task Should_Create_User_And_Return_Valid_RegisterResponse()
    {
        // Arrange
        var registerRequest = new RegisterRequest(
            _faker.Internet.Email(),
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            _faker.Phone.PhoneNumber(),
            _faker.Random.Long(1, 9999),
            _faker.Address.StreetName(),
            _faker.Address.StreetAddress(),
            _faker.Address.SecondaryAddress(),
            _faker.Internet.Password(8)
        );

        var user = new User(
            fullName: new FullName(registerRequest.FirstName, registerRequest.LastName),
            email: new Email(registerRequest.Email),
            address: new Address(registerRequest.Number, registerRequest.NeighBordHood, registerRequest.Road, registerRequest.Complement),
            active: false,
            password: new Password(registerRequest.Password)
        );

        // Mockando o repositório para simular que o e-mail não está registrado
        _mockUserRepository
            .Setup(repo => repo.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<User>());

        // Mockando a criação do usuário
        _mockUserRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Mockando o commit no banco
        _mockDbCommit
            .Setup(commit => commit.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Mockando o envio de e-mail
        _mockEmailService
            .Setup(service => service.SendEmailAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .Returns(Task.CompletedTask);

        var handler = new RegisterHandler(_mockUserRepository.Object, _mockDbCommit.Object, _mapper, _mockEmailService.Object);

        // Act
        var response = await handler.Handle(registerRequest, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.statuscode.Should().Be(201); // Verifica se o código de status é 201
        response.notifications.Should().BeNullOrEmpty(); // Verifica se não há notificações

        // Verifica se os métodos esperados foram chamados
        _mockUserRepository.Verify(repo => repo.CreateAsync(It.Is<User>(u => u.Email.Address == registerRequest.Email), It.IsAny<CancellationToken>()), Times.Once);
        _mockDbCommit.Verify(commit => commit.Commit(It.IsAny<CancellationToken>()), Times.Once);
        _mockEmailService.Verify(service => service.SendEmailAsync(
            user.FullName.FirstName,
            user.Email.Address!,
            "Ative sua Conta!",
            It.IsAny<string>(),
            "ScoreBlog",
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

}
