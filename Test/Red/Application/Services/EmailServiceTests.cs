using FluentAssertions;
using Moq;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Test.Red.Application.Services;

public class EmailServiceRedTests
{
    private readonly Mock<IEmailService> _mockEmailService = new();

    [Fact]
    public async Task Should_Throw_Exception_When_Email_Fails()
    {
        // Arrange
        var toName = "Test User";
        var toEmail = "test@example.com";
        var subject = "Test Subject";
        var body = "This is a test email.";
        var fromName = "Test Sender";
        var fromEmail = "sender@example.com";

        _mockEmailService
            .Setup(es => es.SendEmailAsync(
                toName,
                toEmail,
                subject,
                body,
                fromName,
                fromEmail,
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new Exception("Failed to send email"));

        // Act
        Func<Task> act = async () => await _mockEmailService.Object.SendEmailAsync(
            toName,
            toEmail,
            subject,
            body,
            fromName,
            fromEmail,
            CancellationToken.None
        );

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Failed to send email");

        _mockEmailService.Verify(es => es.SendEmailAsync(
            toName,
            toEmail,
            subject,
            body,
            fromName,
            fromEmail,
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
