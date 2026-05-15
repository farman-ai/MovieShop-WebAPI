using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace MovieShop.WebAPI.Filters;

public class LogCreateMovieRequestFilter : IAsyncActionFilter
{
    private readonly Serilog.ILogger _logger;

    public LogCreateMovieRequestFilter(Serilog.ILogger logger)
    {
        _logger = logger.ForContext<LogCreateMovieRequestFilter>()
            .ForContext("LogType", "CreateMovieRequest");
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.Information(
            "CreateMovie request received from {RemoteIpAddress} with method {Method} and path {Path}. Arguments: {@Arguments}",
            context.HttpContext.Connection.RemoteIpAddress?.ToString(),
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            context.ActionArguments);

        var executedContext = await next();

        _logger.Information(
            "CreateMovie request completed with status code {StatusCode}",
            executedContext.HttpContext.Response.StatusCode);
    }
}
