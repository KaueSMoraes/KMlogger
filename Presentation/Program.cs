
using Domain;
using Presentation.Common.Api;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5070);
});

builder.AddConfiguration();
builder.AddSecurity();
builder.AddCrossOrigin();
builder.AddDataContexts();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseRouting();
app.UseCors(Configuration.CorsPolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();