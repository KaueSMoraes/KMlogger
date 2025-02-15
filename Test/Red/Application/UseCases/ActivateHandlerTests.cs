using System;
using Domain.Interfaces.Repositories;
using Moq;

using  ActivateRequest = Application.UseCases.User.Activate.Request;
using  ActivateHandler = Application.UseCases.User.Activate.Handler;

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
        _mockUserRepository.Setup(r => r.ActivateUserAsync(request.email, request.token, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var handler = new ActivateHandler(_mockUserRepository.Object, _mockDbCommit.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.statuscode.Equals(400));
        _mockDbCommit.Verify(c => c.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }
}
