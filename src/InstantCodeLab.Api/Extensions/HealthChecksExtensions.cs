namespace InstantCodeLab.Api.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }
}
