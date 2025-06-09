using System.Text.Json.Serialization;

namespace InstantCodeLab.Infrastructure.External.Models;

public class Request
{
    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }

    [JsonPropertyName("clientSecret")]
    public string ClientSecret { get; set; }

    [JsonPropertyName("script")]
    public string Script { get; set; }

    [JsonPropertyName("stdin")]
    public string Stdin { get; set; }
    [JsonPropertyName("language")]

    public string Language { get; set; }
    [JsonPropertyName("versionIndex")]

    public string? VersionIndex { get; set; }

    [JsonPropertyName("compileOnly")]
    public bool CompileOnly { get; set; } = true;
}
