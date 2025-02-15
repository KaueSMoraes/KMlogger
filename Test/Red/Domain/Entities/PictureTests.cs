using System;
using Domain.Entities;
using Domain.ValueObjects;

namespace Test.Red.Domain.Entities;

public class PictureTests
{
    [Fact]
    public void Picture_Should_Be_Invalid_When_Properties_Are_Invalid()
    {
        // Arrange
        var fileContent = new byte[5000];  // 5KB (válido)
        var fileStream = new System.IO.MemoryStream(fileContent);
        var appFile = new AppFile(fileStream, "image.jpg"); // AppFile com arquivo válido
        var name = new UniqueName(""); // Nome inválido (vazio)
        const string awsKey = ""; // AWS Key inválida (vazia)
        var urlExpired = DateTime.Now.AddDays(-1); // Data de expiração no passado
        const string urlTemp = ""; // URL inválida (vazia)
        const bool ativo = false;

        var picture = new Picture(appFile, name, awsKey, urlExpired, urlTemp, ativo);

        // Act & Assert
        Assert.False(picture.IsValid);  // O objeto Picture deve ser inválido
    }
}
