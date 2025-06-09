using InstantCodeLab.Domain.Enums;
using InstantCodeLab.Infrastructure.External.Interface;
using InstantCodeLab.Infrastructure.External.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace InstantCodeLab.Infrastructure.External.Service;

public class RemoteCompilerService : IRemoteCompilerService
{
    private readonly IApiService _apiService;
    private readonly IConfiguration _configuration;

    public RemoteCompilerService(IApiService apiService, IConfiguration configuration)
    {
        _apiService = apiService;
        _configuration = configuration;
    }

    public async Task<Response> SendPostRequest(string code, KeyValuePair<LanguageCode, int> languageVersion, string input)
    {
        Request request = new Request
        {
            ClientId = _configuration.GetValue<string>("Jdoodle:ClientId"),
            ClientSecret = _configuration.GetValue<string>("Jdoodle:ClientSecret"),
            Script = code,
            Language = languageVersion.Key.ToString(),
            VersionIndex = languageVersion.Value.ToString() ?? "0",
            CompileOnly = false,
            Stdin = input
        };

        string response = await _apiService.SendPostRequest(JsonSerializer.Serialize(request));
        Response result = JsonSerializer.Deserialize<Response>(response);

        return result;
    }
}
