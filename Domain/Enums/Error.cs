namespace Domain.Enums;

public  enum Error
{
    ServerError, 
    DatabaseConnectionFailed, 
    NullReferenceException, 
    FileNotFound, 
    Unauthorized, 
    BadRequest, 
    Timeout, 
    DependencyFailure, 
    ConfigurationError, 
    APIError, 
    ResourceLimitExceeded 
}