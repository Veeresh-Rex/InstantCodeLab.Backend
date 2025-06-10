namespace InstantCodeLab.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiCors(this IServiceCollection services, string allowedHosts)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                  policy => policy
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });
    }
}
