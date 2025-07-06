using Microsoft.Extensions.DependencyInjection;
using InstantCodeLab.Domain.Repositories;
using InstantCodeLab.Infrastructure.Persistence.Repositories;
using InstantCodeLab.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InstantCodeLab.Infrastructure.Extensions;

public static class PersistanceExtension
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddScoped<ILabRoomRepository, LabRoomRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
