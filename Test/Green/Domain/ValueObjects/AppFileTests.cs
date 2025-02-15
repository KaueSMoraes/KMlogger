using System;
using Domain.ValueObjects;

namespace Test.Green.Domain.ValueObjects;

public sealed class AppFileTests
{
    [Fact]
    public void AppFile_Should_Be_Valid_When_Valid_Properties_Are_Provided()
    {
        var fileContent = new byte[5000];  // 5KB (válido)
        var fileStream = new MemoryStream(fileContent);
        var fileName = "valid-file.pdf";

        var appFile = new AppFile(fileStream, fileName);

        Assert.True(appFile.IsValid); 
        Assert.Equal(fileName, appFile.FileName); 
        Assert.Equal(fileContent.Length, appFile.FileSize); 
    }
}