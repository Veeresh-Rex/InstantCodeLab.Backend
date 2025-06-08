using System.Net.Http;
using System.Threading.Tasks;
using InstantCodeLab.Infrastructure.External.Interface;

namespace InstantCodeLab.Infrastructure.External.Service;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SendPostRequest(string body)
    {
        var requestContent = new StringContent(body);
        string url = "https://api.jdoodle.com/v1/execute";

        var response = await _httpClient.PostAsync(url, requestContent);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
