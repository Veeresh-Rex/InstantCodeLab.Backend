namespace InstantCodeLab.Application.DTOs;

public class CompileResponseDto
{
    public string Output{ get; set; } = string.Empty;
    public bool IsError { get; set; } = false;
}
