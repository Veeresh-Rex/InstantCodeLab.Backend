namespace InstantCodeLab.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiCors(this IServiceCollection services, string allowedHosts)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("frontend", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });
    }
}
