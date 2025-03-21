using System;
using Domain.ValueObjects;

namespace Domain.Entities;

[Table("Categories")]
public  class Category : Entity
{
    public UniqueName? Name { get; private set; }
    public bool? Active { get; private set; }
    public List<App>? Apps {get; private set;}

    private Category(){}
    public Category(UniqueName? name, bool? active)
    {
        AddNotificationsFromValueObjects(name);
        Name = name;
        Active = active;
    }
}
