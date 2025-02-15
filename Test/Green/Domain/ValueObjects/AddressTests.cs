using System;
using Domain.ValueObjects;

namespace Test.Green.Domain.ValueObjects;

public sealed class AddressTests
{
    [Fact]
    public void Address_Should_Be_Valid_When_Valid_Properties_Are_Provided()
    {
        var address = new Address(123, "Downtown", "Main St", "Apt 101");
        Assert.True(address.IsValid); 
    }
}