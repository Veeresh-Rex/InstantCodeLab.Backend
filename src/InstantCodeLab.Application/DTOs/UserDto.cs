namespace InstantCodeLab.Application.DTOs;

public class UserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int UserType { get; set; } = 0; // 0: User, 1: Admin
    public string LabRoomId { get; set; } = string.Empty;
}
