using InstantCodeLab.Api.ExceptionHandlers;

namespace InstantCodeLab.Api.Extensions;

public static class ExceptionHandlerExtentions
{
    public static IServiceCollection AddExceptionHandlerServices(this IServiceCollection services)
    {
        // Add exception handler services here

        services.AddExceptionHandler<GlobalExecptionHandler>(); // Should be the last one
        return services;
    }

}
