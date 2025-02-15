namespace Domain;

internal static class Configuration
{
    internal const int DefaultStatusCode = 200;
    internal const int DefaultPageNumber = 1;
    internal const int DefaultPageSize = 25;

    internal static string BackendUrl { get; set; } = string.Empty;
    internal static string FrontendUrl { get; set; } = string.Empty;
    internal static string SmtpServer { get; set; } = string.Empty;
    internal static string JwtKey { get; set; } = string.Empty;
    internal static int SmtpPort { get; set; } = 587;
    internal static string VersionApi { get; set; } = string.Empty;
    internal static string Database { get; set; } = string.Empty;
    internal static string UserNameDatabase { get; set; } = string.Empty;
    internal static string HostDatabase { get; set; } = string.Empty;
    internal static string PassWordDatabase { get; set; } = string.Empty;
    internal static int PortDatabase { get; set; } = 0;
    internal static string SmtpUser { get; set; } = string.Empty;
    internal static string SmtpPass { get; set; } = string.Empty;
    internal static string AwsKeyId { get; set; } = string.Empty;
    internal static string ApiKey { get; set; } = string.Empty;
    internal static string ApiKeyAttribute { get; set; } = string.Empty;
    internal static string AwsKeySecret { get; set; } = string.Empty;
    internal static string AwsRegion { get; set; } = string.Empty;  
    internal static string BucketImages { get; set; } = string.Empty;  
    internal static string BucketVideos { get; set; } = string.Empty;  
    internal static int DurationUrlTempVideos { get; set; } = 24;
    internal static bool IsDevelopment { get; set; } = true;
    internal static string CorsPolicyName { get; set; } = "ScoreblogCors";
    internal static int DurationUrlTempImage { get; set; } = 24;
}