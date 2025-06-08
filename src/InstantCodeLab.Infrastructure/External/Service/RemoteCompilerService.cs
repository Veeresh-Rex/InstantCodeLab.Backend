using InstantCodeLab.Domain.Enums;
using InstantCodeLab.Infrastructure.External.Interface;
using InstantCodeLab.Infrastructure.External.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace InstantCodeLab.Infrastructure.External.Service;

public class RemoteCompilerService : IRemoteCompilerService
{
    private readonly IApiService _apiService;

    public RemoteCompilerService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<string> SendPostRequest(string code, KeyValuePair<LanguageCode, int> languageVersion, string input)
    {
        Request request = new Request
        {
            Script = code,
            Language = languageVersion.Key.ToString(),
            VersionIndex = languageVersion.Value.ToString() ?? "0",
            CompileOnly = true,
            Stdin = input
        };

        string response = await _apiService.SendPostRequest(JsonSerializer.Serialize(request));
        Response result = JsonSerializer.Deserialize<Response>(response);

        return result.Output;
    }
}
