using System;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Role : Entity
{
    public UniqueName Name { get; private set; }
    public string Slug { get; private set; }
    public IList<User>? Users { get;  private set; }
    
    private Role(){}
    public Role(UniqueName name, string slug, IList<User> users)
    {
        AddNotificationsFromValueObjects(name);
        Name = name;
        Slug = slug;
        Users = users;
    }
}