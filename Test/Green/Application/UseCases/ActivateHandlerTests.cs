using System;
using Domain.Interfaces.Repositories;
using Moq;

using  ActivateRequest = Application.UseCases.User.Activate.Request;
using  ActivateHandler = Application.UseCases.User.Activate.Handler;

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
        _mockUserRepository.Setup(r => r.ActivateUserAsync(request.email, request.token, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockDbCommit.Setup(c => c.Commit(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var handler = new ActivateHandler(_mockUserRepository.Object, _mockDbCommit.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.statuscode.Equals(200));
        _mockDbCommit.Verify(c => c.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
