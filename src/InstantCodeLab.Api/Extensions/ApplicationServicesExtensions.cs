
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Application.Services;
using InstantCodeLab.Infrastructure.Utilities;

namespace InstantCodeLab.Api.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add application services here
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ICompilerService, CompilerService>();
        services.AddSingleton<CodeStore>();
        services.AddHostedService<CodeSyncService>();

        return services;
    }
}
