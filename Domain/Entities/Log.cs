using System;
using Domain.Enums;
using Domain.ValueObjects;

using Environment = Domain.Enums.Environment;

namespace Domain.Entities;

[Table("Logs")]
public  class LogEnrty : Entity
{
    public Environment Environment { get; private set; }  
    public Level? Level { get; private set; }  
    public Description? Message { get; private set; }
    public Description? StackTrace { get; private set; } 
    private LogEnrty(){}
    public LogEnrty(Environment environment, Level? level, Description? message, Description? stackTrace)
    {
        AddNotificationsFromValueObjects(message, stackTrace);
        Environment = environment;
        Level = level;
        Message = message;
        StackTrace = stackTrace;
    }
}


