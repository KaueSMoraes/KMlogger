using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;
public static class ServicesExtensions
{
    public static void AppServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}