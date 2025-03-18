namespace Domain;

public static class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;

    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
    public static string SmtpServer { get; set; } = string.Empty;
    public static string JwtKey { get; set; } = string.Empty;
    public static int SmtpPort { get; set; } = 587;
    public static string ConnectionStringClickHouse { get; set; } = string.Empty;
    public static string VersionApi { get; set; } = string.Empty;
    public static string SmtpUser { get; set; } = string.Empty;
    public static string SmtpPass { get; set; } = string.Empty;
    public static string AwsKeyId { get; set; } = string.Empty;
    public static string ApiKey { get; set; } = string.Empty;
    public static string ApiKeyAttribute { get; set; } = string.Empty;
    public static string SqliteConnectionString { get; set; } = string.Empty;    
    public static bool IsDevelopment { get; set; } = true;
    public static string CorsPolicyName { get; set; } = "KmLoggerCorsPolicy";
}