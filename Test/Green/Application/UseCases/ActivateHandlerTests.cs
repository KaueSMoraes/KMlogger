using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

using ActivateRequest = Application.UseCases.User.Activate.Request;
using ActivateHandler = Application.UseCases.User.Activate.Handler;

namespace Test.Green.Application.UseCases;

public class ActivateHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IDbCommit> _mockDbCommit;

    public ActivateHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockDbCommit = new Mock<IDbCommit>();
    }

    [Fact]
    public async Task Should_Activate_User_And_Commit()
    {
        // Arrange
        var request = new ActivateRequest("test@example.com", 1234);

        // Criando um objeto User válido para o mock
        var mockUser = new User(
            new FullName("Test", "User"), 
            new Email("test@example.com"), 
            new Address(123, "Rua Teste", "123", "Cidade"), 
            false, 
            new Password("Test@123")
        );

        // Forçar o mesmo token do request para garantir correspondência no teste
        mockUser.GenerateNewToken();
        
        // Mockando o ActivateUserAsync para retornar o usuário correto
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
        Assert.True(result.statuscode.Equals(200));
        _mockDbCommit.Verify(c => c.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
