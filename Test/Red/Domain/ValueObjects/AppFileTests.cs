using System;
using Domain.ValueObjects;

namespace Test.Red.Domain.ValueObjects;

public class AppFileTests
{
    [Fact]
    public void AppFile_Should_Be_Invalid_When_Properties_Are_Invalid()
    {
        // Arrange
        var fileContent = new byte[10_000_001];  // 10MB + 1 byte (inválido)
        var fileStream = new MemoryStream(fileContent);
        var fileName = "";  // Nome inválido

        // Criando o AppFile com valores inválidos
        var appFile = new AppFile(fileStream, fileName);

        // Act & Assert
        Assert.False(appFile.IsValid); // O objeto deve ser inválido
        Assert.Contains(appFile.Notifications, n => n.Message == "File size must be less than 10MB");
        Assert.Contains(appFile.Notifications, n => n.Message == "File name cannot be null or empty");
    }
}