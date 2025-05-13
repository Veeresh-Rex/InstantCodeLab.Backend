using Microsoft.Extensions.Primitives;

namespace InstantCodeLab.Api.Middleware;

public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;
    public CorrelationIdMiddleware(
            RequestDelegate next,
            ILogger<CorrelationIdMiddleware> logger
)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        bool hasCorrelationId = context.Request.Headers.TryGetValue(CorrelationIdHeader, out StringValues correlationId);
        if (hasCorrelationId && !string.IsNullOrWhiteSpace(correlationId))
        {
            context.TraceIdentifier = correlationId!;
        }

        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationIdHeader] = correlationId;
            return Task.CompletedTask;
        });


        using (_logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
        {
            await _next(context);
        }
    }
}


