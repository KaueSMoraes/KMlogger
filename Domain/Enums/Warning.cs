using System;

namespace Domain.Enums;

public  enum Warning
{
    CodeVulnerability, 
    SqlInjection, 
    SlowResponseTime, 
    DeprecatedAPIUsage, 
    HighMemoryUsage, 
    HighCPUUsage, 
    UnauthorizedAccessAttempt, 
    SuspiciousRequestPattern 
}