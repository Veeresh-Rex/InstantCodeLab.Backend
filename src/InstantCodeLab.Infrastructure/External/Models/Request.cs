namespace InstantCodeLab.Infrastructure.External.Models;

public class Request
{
    public string Script { get; set; }
    public string Stdin { get; set; }
    public string Language { get; set; }
    public string VersionIndex { get; set; }
    public bool CompileOnly { get; set; } = true;
}
