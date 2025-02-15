using System;
using Domain.ValueObjects;

namespace Test.Red.Domain.ValueObjects;

public class UniqueNameTests
{
    [Fact]
    public void UniqueName_Should_Be_Invalid_When_Invalid_Name_Provided()
    {
        var uniqueName = new UniqueName(""); 
        Assert.False(uniqueName.IsValid); 
        Assert.Contains(uniqueName.Notifications, n => n.Message == "Name cannot be null or empty");
    }
}