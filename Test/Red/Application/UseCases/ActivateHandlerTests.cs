using System;
using Domain.Interfaces.Repositories;
using Moq;

using  ActivateRequest = Application.UseCases.User.Activate.Request;
using  ActivateHandler = Application.UseCases.User.Activate.Handler;
using Domain.Entities;
using Domain.ValueObjects;

namespace Test.Red.Application.UseCases;

public class ActivateHandlerRedTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IDbCommit> _mockDbCommit;

    public ActivateHandlerRedTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockDbCommit = new Mock<IDbCommit>();
    }

    [Fact]
    public async Task Should_Return_False_When_Activation_Fails()
    {
        // Arrange
        var request = new ActivateRequest("invalid@example.com", 5678);
        
         var mockUser = new User(
            new FullName("Test", "User"), 
            new Email("test@example.com"), 
            new Address(123,"Rua Teste", "123", "Cidade"), 
            false, 
            new Password("Test@123")
        );
        
         _mockUserRepository
            .Setup(r => r.ActivateUserAsync(request.email, request.token, It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockUser); // Retorna um usuário válido

        _mockDbCommit
            .Setup(c => c.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new ActivateHandler(_mockUserRepository.Object, _mockDbCommit.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.statuscode.Equals(400));
        _mockDbCommit.Verify(c => c.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }
}
