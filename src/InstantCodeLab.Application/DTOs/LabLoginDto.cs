using InstantCodeLab.Domain.Entities;

namespace InstantCodeLab.Application.DTOs;

public class LabLoginDto
{
    public string Username { get; set; }
    public string? RetrivalId { get; set; }
    public string? Password { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = true;
    public string? AdminPin { get; set; } = string.Empty;
}

public class UserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public bool IsAdmin { get; set; } = true;
    public string Code { get; set; } = string.Empty;
    public string JoinedLabRoomName { get; set; }
    public string JoinedLabRoomId { get; set; }
    public string Language { get; set; }
    public static UserDto ConvertToUser(User? user)
    {
        if(user is null)
        {
            return null;
        }

        return new UserDto()
        {
            Username = user.Username,
            Code = user.OwnCode,
            Id = user.Id,
            IsAdmin = user.IsAdmin,
            JoinedLabRoomId = user.LabRoomId
        };
    }
}
