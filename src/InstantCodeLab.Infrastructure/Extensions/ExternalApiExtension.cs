using System;
using InstantCodeLab.Infrastructure.External.Interface;
using InstantCodeLab.Infrastructure.External.Service;
using Microsoft.Extensions.DependencyInjection;

namespace InstantCodeLab.Infrastructure.Extensions;

public static class ExternalApiExtension
{
    public static IServiceCollection AddExternalCompilerApi(this IServiceCollection services)
    {
        services.AddHttpClient<IApiService, ApiService>((client) =>
        {
            client.BaseAddress = new Uri("https://api.jdoodle.com/");
        });

        services.AddScoped<IRemoteCompilerService, RemoteCompilerService>();

        return services;
    }
}
