using System;
using Domain.Enums;
using Domain.ValueObjects;

using Environment = Domain.Enums.Environment;

namespace Domain.Entities;

public  class Log : Entity
{
    public Environment Environment { get; private set; }  
    public Enum? Level { get; private set; }  
    public Description? Message { get; private set; }
    public Description? StackTrace { get; private set; } 
    private Log(){}
    public Log(Environment environment, Enum? level, Description? message, Description? stackTrace)
    {
        AddNotificationsFromValueObjects(message, stackTrace);
        Environment = environment;
        Level = level;
        Message = message;
        StackTrace = stackTrace;
    }
}


