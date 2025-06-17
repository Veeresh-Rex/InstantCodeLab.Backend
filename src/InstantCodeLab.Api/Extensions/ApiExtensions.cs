namespace InstantCodeLab.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiCors(this IServiceCollection services, string allowedHosts)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "frontend",
                policy =>
                {
                    policy
                        .WithOrigins("https://instant-code-lab.vercel.app", "http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });
    }
}
