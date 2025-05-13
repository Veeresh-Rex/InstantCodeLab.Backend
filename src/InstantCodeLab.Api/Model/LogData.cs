using Newtonsoft.Json;

namespace InstantCodeLab.Api.Model;

public class LogData
{
    public string? RequestMethod { get; set; }
    public string? RequestUrl { get; set; }
    public IDictionary<string, string>? RequestHeaders { get; set; }
    public string? RequestBody { get; set; }
    public int? StatusCode { get; set; }
    public IDictionary<string, string>? ResponseHeaders { get; set; }
    public string? ResponseBody { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }
}

