using System;
using Domain.ValueObjects;

namespace Test.Green.Domain.ValueObjects;

public class UniqueNameTests
{
    [Fact]
    public void UniqueName_Should_Be_Valid_When_Valid_Name_Provided()
    {
        var uniqueName = new UniqueName("UniqueName123");
        Assert.True(uniqueName.IsValid); 
    }
}
