using System;
using Domain.ValueObjects;

namespace Test.Red.Domain.ValueObjects;

public class FullNameTests
{
    [Fact]
    public void FullName_Should_Be_Invalid_When_Invalid_Properties_Provided()
    {
        var fullName = new FullName("", ""); 
        Assert.False(fullName.IsValid); 
        Assert.Contains(fullName.Notifications, n => n.Message == "First name cannot be null or empty");
        Assert.Contains(fullName.Notifications, n => n.Message == "Last name cannot be null or empty");
    }
}