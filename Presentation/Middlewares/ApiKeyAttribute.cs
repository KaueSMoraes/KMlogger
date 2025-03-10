using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Middlewares;

[AttributeUsage(validOn: AttributeTargets.Method)]
public class ApiKey : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(Configuration.ApiKeyAttribute, out var exTractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api_Key not found"
            };
            return;
        }
        
        if (!Configuration.ApiKey.Equals(exTractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Invalid api_key"
            };
            return;
        }
        await next();
    }
}