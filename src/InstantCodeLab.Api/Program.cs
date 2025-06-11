using InstantCodeLab.Api.Extensions;
using InstantCodeLab.Api.Hubs;
using InstantCodeLab.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAppHealthChecks();
builder.Services.AddSwagger();
builder.Services.AddApiCors(builder.Configuration["AllowedHosts"] ?? string.Empty);
builder.Services.AddPersistance();
builder.Services.AddSignalR();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlerServices();
builder.Services.AddExternalCompilerApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHeartbeat();

app.UseCors("frontend");

app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("/message");

app.MapGet("/", () => "Hello World from InstantCodeLab API!");

await app.RunAsync();


public partial class Program
{
    protected Program()
    {
    }
}