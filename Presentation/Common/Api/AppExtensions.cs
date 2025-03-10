using System;

namespace Presentation.Common.Api;

public static class AppExtensions
{
    #region ConfigureEnvironment
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseForwardedHeaders();
    }
    #endregion ConfigureEnvironment

    #region Security
    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
    #endregion
}