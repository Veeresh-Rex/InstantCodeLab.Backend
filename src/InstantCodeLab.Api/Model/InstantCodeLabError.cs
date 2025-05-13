using Microsoft.AspNetCore.Mvc;

namespace InstantCodeLab.Api.Model;

public class InstantCodeLabError : ProblemDetails
{
    public IDictionary<string, string[]>? Errors { get; set; }
}
