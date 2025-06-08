using InstantCodeLab.Domain.Enums;

namespace InstantCodeLab.Application.DTOs;

public class CompileResquestDto
{
    public string Code { get; set; }
    public string StdinInput { get; set; }
    public LanguageCode Language { get; set; }
}
