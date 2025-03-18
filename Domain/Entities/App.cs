using System;
using Domain.ValueObjects;

using Environment = Domain.Enums.Environment;

namespace Domain.Entities;

public  class App : Entity
{
    public UniqueName? Name { get; private set; }
    public Category Category { get; private set; }
    public Environment? Environment { get; private set; }
    public List<LogEnrty>? Logs {get; private set;}
    public bool? Active { get; private set; }

    private App(){}
    public App( UniqueName? name, Category? category,
         Environment? environment, List<LogEnrty> logs, bool? active)
    {
        Name = name;
        Category = category;
        Environment = environment;
        Logs = logs;
        Active = active;
    }
}
