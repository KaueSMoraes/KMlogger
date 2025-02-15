using System;
using Domain.ValueObjects;

namespace Test.Red.Domain.ValueObjects;

public class DescriptionTests
{
    [Fact]
    public void Description_Should_Be_Invalid_When_Invalid_Text_Provided()
    {
        var description = new Description(""); 
        Assert.False(description.IsValid); 
        Assert.Contains(description.Notifications, n => n.Message == "Description cannot be null or empty");
    }
}