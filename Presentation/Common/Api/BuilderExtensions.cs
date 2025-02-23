using System;
using System.Text;
using System.Text.Json.Serialization;
using Domain;
using Application;
using Infrastructure.Data;
using Infrastructure.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Application.DI;

namespace Presentation.Common.Api;

internal static class BuilderExtensions
{
    internal static void AddConfiguration(
        this WebApplicationBuilder builder)
    {
        Configuration.IsDevelopment = builder.Environment.IsDevelopment();
        Configuration.JwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty;
        Configuration.BackendUrl = Environment.GetEnvironmentVariable("BACKEND_URL") ?? string.Empty;
        Configuration.VersionApi = Environment.GetEnvironmentVariable("VERSION_API") ?? string.Empty;
        Configuration.ApiKey = Environment.GetEnvironmentVariable("API_KEY") ?? string.Empty;
        Configuration.ApiKeyAttribute = "X-API-KEY";
        Configuration.FrontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:4200";
        Configuration.AwsKeyId = Environment.GetEnvironmentVariable("AWS_KEY_ID") ?? string.Empty;
        Configuration.AwsKeySecret = Environment.GetEnvironmentVariable("AWS_KEY_SECRET") ?? string.Empty;
        Configuration.AwsRegion = Environment.GetEnvironmentVariable("AWS_REGION") ?? string.Empty;
        Configuration.BucketImages = Environment.GetEnvironmentVariable("BUCKET_IMAGES") ?? string.Empty;
        Configuration.SmtpUser = Environment.GetEnvironmentVariable("SMTP_USER") ?? string.Empty;
        Configuration.SmtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? string.Empty;
        Configuration.SmtpPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var smtpPort) ? smtpPort : 587;
        Configuration.SmtpPass = Environment.GetEnvironmentVariable("SMTP_PASS") ?? string.Empty;
        Configuration.BucketVideos = Environment.GetEnvironmentVariable("BUCKET_VIDEOS") ?? string.Empty;
        Configuration.DurationUrlTempVideos = 24;
        Configuration.DurationUrlTempImage = 24;
        Configuration.Database = Environment.GetEnvironmentVariable("DATABASE") ?? string.Empty;
        Configuration.UserNameDatabase = Environment.GetEnvironmentVariable("USERNAME_DATABASE") ?? string.Empty;
        Configuration.HostDatabase = Environment.GetEnvironmentVariable("HOST_DATABASE") ?? string.Empty;
        Configuration.PassWordDatabase = Environment.GetEnvironmentVariable("PASSWORD_DATABASE") ?? string.Empty;
        Configuration.PortDatabase = int.TryParse(Environment.GetEnvironmentVariable("PORT_DATABASE"), out var portdatabase) ? portdatabase : 5432;
        builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
        options.ReturnHttpNotAcceptable = true;
    }).EnableInternalControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;})
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    })
    .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
            };
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
    }

    internal static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        builder.Services.AddAuthorization();
    }

    internal static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<KMLoggerDbContext>(
                x => { x.UseNpgsql(StringConnection.BuildConnectionString()); });
    }

    internal static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                Configuration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl,
                        Configuration.FrontendUrl.Replace("http://", "https://"),
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    internal static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownProxies.Clear();
        });

        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 1024 * 1024 * 500;
        });
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 1024 * 1024 * 500;
        });
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });
        
        builder.Services.AppServices();
        builder.Services.ConfigureInfraServices();
    }
}