using System;
using Flunt.Br;

namespace Domain.ValueObjects;

public class UniqueName : BaseValueObject
{
    public string Name { get; private set; }
    public UniqueName(string name)
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotNullOrEmpty(name, Key, "Name cannot be null or empty")
                .IsLowerThan(name.Length, 50.0, Key, "Name cannot be longer than 50 characters")
            );
        Name = name;
    }
    private UniqueName(){}
}