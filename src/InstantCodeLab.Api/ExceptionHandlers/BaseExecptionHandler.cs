using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InstantCodeLab.Api.Model;

namespace InstantCodeLab.Api.ExceptionHandlers;

public abstract class BaseExecptionHandler<IException> : IExceptionHandler where IException : Exception
{
    public abstract IDictionary<string, string[]> GetErrorMessage(IException exception);
    public abstract string ExceptionType { get; }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not IException)
        {
            return false;
        }

        ProblemDetails problemDetails = CreateProblemDetails(exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private InstantCodeLabError CreateProblemDetails(Exception exception) => new()
    {
        Title = "Some problem occered",
        Status = (int)HttpStatusCode.BadRequest,
        Type = ExceptionType ?? "Order management api",
        Errors = GetErrorMessage(exception as IException)
    };
}
