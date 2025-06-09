using System.Net.Http;
using System.Text;
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
        var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("v1/execute", requestContent);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (HttpRequestException ex)
        {
            // Log the exception or handle it as needed
            throw new System.Exception("Error while sending request to API", ex);
        }
        catch (System.Exception ex)
        {
            // Handle other exceptions
            throw new System.Exception("An unexpected error occurred", ex);
        }
    }
}
