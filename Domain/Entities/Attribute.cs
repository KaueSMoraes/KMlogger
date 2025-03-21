using System;

namespace Domain.Entities;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TableAttribute : Attribute
{
    public string Name { get; }
    public TableAttribute(string name)
    {
        Name = name;
    }
}