namespace InstantCodeLab.Application.DTOs;

public class LabLoginDto
{
    public string Username { get; set; }
    public string Password { get; set; } = "12345";
    public bool IsAdmin { get; set; } = true;
    public string AdminPin { get; set; } = "1234";
}

public class LabLoginResponseDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public bool IsAdmin { get; set; } = true;
    public string Code { get; set; } = string.Empty;
    public string LabRoomName { get; set; }
}
