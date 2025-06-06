
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Application.Services;

namespace InstantCodeLab.Api.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add application services here
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoomService, RoomService>();


        return services;
    }
}
