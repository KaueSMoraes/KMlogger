using System;
using Domain.ValueObjects;

namespace Domain.Entities;

internal class Role : Entity
{
    internal UniqueName Name { get; private set; }
    internal string Slug { get; private set; }
    internal IList<User>? Users { get;  private set; }
    
    private Role(){}
    internal Role(UniqueName name, string slug, IList<User> users)
    {
        AddNotificationsFromValueObjects(name);
        Name = name;
        Slug = slug;
        Users = users;
    }
}