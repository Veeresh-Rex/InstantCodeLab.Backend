using System.Text.Json.Serialization;

namespace InstantCodeLab.Infrastructure.External.Models;

public class Response
{
    [JsonPropertyName("output")]
    public string Output { get; set; }

    [JsonPropertyName("isExecutionSuccess")]
    public bool IsExecutionSuccess { get; set; }
}
