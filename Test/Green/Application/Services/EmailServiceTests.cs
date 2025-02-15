using System;
using Domain.Interfaces.Services;
using Moq;

namespace Test.Green.Application.Services;

public class EmailServiceTests
{
    private readonly Mock<IEmailService> _mockEmailService = new();

    [Fact]
    public async Task Should_Send_Email_Successfully()
    {
        // Arrange
        const string toName = "Test User";
        const string toEmail = "test@example.com";
        const string subject = "Test Subject";
        const string body = "This is a test email.";
        const string fromName = "Test Sender";
        const string fromEmail = "sender@example.com";

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
            .Returns(Task.CompletedTask);

        // Act
        await _mockEmailService.Object.SendEmailAsync(
            toName,
            toEmail,
            subject,
            body,
            fromName,
            fromEmail,
            CancellationToken.None
        );

        // Assert
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
