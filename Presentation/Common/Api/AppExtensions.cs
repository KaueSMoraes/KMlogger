using System;

namespace Presentation.Common.Api;

internal static class AppExtensions
{
    #region ConfigureEnvironment
    internal static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseForwardedHeaders();
    }
    #endregion ConfigureEnvironment

    #region Security
    internal static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
    #endregion
}