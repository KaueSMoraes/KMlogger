using System;
using Domain.ValueObjects;

namespace Test.Green.Domain.ValueObjects;

public class DescriptionTests
{
    [Fact]
    public void Description_Should_Be_Valid_When_Valid_Text_Provided()
    {
        var description = new Description("This is a valid description");
        Assert.True(description.IsValid); 
    }
}