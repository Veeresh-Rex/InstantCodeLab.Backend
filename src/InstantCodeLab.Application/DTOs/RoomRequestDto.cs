namespace InstantCodeLab.Application.DTOs;

public class RoomRequestDto
{
    public string LabName { get; set; } = "InstantCodeLab";
    public string? Password { get; set; }
    public string? AdminPin { get; set; }
}

public class RoomResponseDto
{
    public string? AdminUrl { get; set; }
    public string? MembersUrl { get; set; }
}

public class GetRoomResponseDto
{
    public string Id { get; set; }
    public string LabName { get; set; }
    public bool IsRoomPasswordEnabled { get; set; }
    public bool IsAdminPinEnabled { get; set; }
}
