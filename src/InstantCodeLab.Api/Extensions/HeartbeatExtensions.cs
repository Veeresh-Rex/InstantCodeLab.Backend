namespace InstantCodeLab.Api.Extensions;

public static class HeartbeatExtensions
{
    public static void MapHeartbeat(this WebApplication application, string pattern = "/heartbeat")
    {
        application.MapGet(pattern, () => "beating").ShortCircuit();
    }
}
