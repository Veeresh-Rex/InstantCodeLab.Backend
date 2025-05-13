using System.Text;
using InstantCodeLab.Api.Model;

namespace InstantCodeLab.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequestAsync(context);

        var originalResponseBody = context.Response.Body;
        using (var responseBodyStream = new MemoryStream())
        {
            context.Response.Body = responseBodyStream;

            await _next(context);

            // After the response has been written, log it
            await LogResponseAsync(context);

            await responseBodyStream.CopyToAsync(originalResponseBody);
        }
    }

    private async Task LogRequestAsync(HttpContext context)
    {
        var request = context.Request;
        var requestBody = await ReadRequestBodyAsync(request);

        var logData = new LogData
        {
            RequestMethod = request.Method,
            RequestUrl = request.Path + request.QueryString,
            RequestHeaders = request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            RequestBody = requestBody
        };

        _logger.LogInformation("Request Details: {@LogData}", logData);
    }

    private async Task LogResponseAsync(HttpContext context)
    {
        var response = context.Response;
        var responseBody = await FormatResponse(response);

        var responseLog = new LogData
        {
            StatusCode = response.StatusCode,
            ResponseHeaders = response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            RequestBody = responseBody
        };

        _logger.LogInformation("Response Details: {@ResponseLog}", responseLog);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();

        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }

    private static async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(response.Body).ReadToEndAsync();

        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }
}
