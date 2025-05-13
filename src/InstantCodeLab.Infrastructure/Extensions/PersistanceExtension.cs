using Microsoft.Extensions.DependencyInjection;
using InstantCodeLab.Domain.Repositories;
using InstantCodeLab.Infrastructure.Persistence.Repositories;

namespace InstantCodeLab.Infrastructure.Extensions;

public static class PersistanceExtension
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();
        return services;
    }
}
