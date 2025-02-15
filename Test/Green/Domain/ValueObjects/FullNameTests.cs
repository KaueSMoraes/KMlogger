using System;
using Domain.ValueObjects;

namespace Test.Green.Domain.ValueObjects;

public class FullNameTests
{
    [Fact]
    public void FullName_Should_Be_Valid_When_Valid_Properties_Provided()
    {
        var fullName = new FullName("John", "Doe");
        Assert.True(fullName.IsValid); 
    }
}