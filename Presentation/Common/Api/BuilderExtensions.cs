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
using Nest;

namespace Presentation.Common.Api;

public  static class BuilderExtensions
{
    public static void AddConfiguration(
        this WebApplicationBuilder builder)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        Configuration.IsDevelopment = builder.Environment.IsDevelopment();
        Configuration.JwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty;
        Configuration.BackendUrl = Environment.GetEnvironmentVariable("BACKEND_URL") ?? string.Empty;
        Configuration.VersionApi = Environment.GetEnvironmentVariable("VERSION_API") ?? string.Empty;
        Configuration.ApiKey = Environment.GetEnvironmentVariable("API_KEY") ?? string.Empty;
        Configuration.ApiKeyAttribute = "X-API-KEY";
        Configuration.FrontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:4200";
        Configuration.SmtpUser = Environment.GetEnvironmentVariable("SMTP_USER") ?? string.Empty;
        Configuration.SmtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? string.Empty;
        Configuration.SmtpPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var smtpPort) ? smtpPort : 587;
        Configuration.SmtpPass = Environment.GetEnvironmentVariable("SMTP_PASS") ?? string.Empty;
        Configuration.SqliteConnectionString = "Data Source=" + Path.GetFullPath(Path.Combine(projectRoot, "Infrastructure", "dbkmlogger.db"));
        builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
        options.ReturnHttpNotAcceptable = true;
    }).ConfigureApiBehaviorOptions(options =>
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

    public static void AddSecurity(this WebApplicationBuilder builder)
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

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<KMLoggerDbContext>(
                x => { x.UseSqlite(Configuration.SqliteConnectionString); });

        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("logs");
        var elasticClient = new ElasticClient(settings);
        builder.Services.AddSingleton(elasticClient);
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
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

    public static void AddServices(this WebApplicationBuilder builder)
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