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
    public static string VersionApi { get; set; } = string.Empty;
    public static string Database { get; set; } = string.Empty;
    public static string UserNameDatabase { get; set; } = string.Empty;
    public static string HostDatabase { get; set; } = string.Empty;
    public static string PassWordDatabase { get; set; } = string.Empty;
    public static int PortDatabase { get; set; } = 0;
    public static string SmtpUser { get; set; } = string.Empty;
    public static string SmtpPass { get; set; } = string.Empty;
    public static string AwsKeyId { get; set; } = string.Empty;
    public static string ApiKey { get; set; } = string.Empty;
    public static string ApiKeyAttribute { get; set; } = string.Empty;
    public static string AwsKeySecret { get; set; } = string.Empty;
    public static string AwsRegion { get; set; } = string.Empty;  
    public static string BucketImages { get; set; } = string.Empty;  
    public static string BucketVideos { get; set; } = string.Empty;  
    public static int DurationUrlTempVideos { get; set; } = 24;
    public static bool IsDevelopment { get; set; } = true;
    public static string CorsPolicyName { get; set; } = "ScoreblogCors";
    public static int DurationUrlTempImage { get; set; } = 24;
}