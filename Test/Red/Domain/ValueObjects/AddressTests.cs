using System;
using Domain.ValueObjects;

namespace Test.Red.Domain.ValueObjects;

public class AddressTests
{
    [Fact]
    public void Address_Should_Be_Invalid_When_Properties_Are_Invalid()
    {
        var address = new Address(-1, "", null, "This is a very long complement that exceeds 100 characters, which should make the validation fail because the complement is too long.");
        Assert.False(address.IsValid);
        
        Assert.Contains(address.Notifications, n => n.Message == "Number must be greater than 0");
        Assert.Contains(address.Notifications, n => n.Message == "Road is required");
        Assert.Contains(address.Notifications, n => n.Message == "Neighborhood is required");
        Assert.Contains(address.Notifications, n => n.Message == "Complement must not exceed 100 characters");
    }
}