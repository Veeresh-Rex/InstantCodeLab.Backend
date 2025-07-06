using System;
namespace InstantCodeLab.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string OwnCode { get; set; } = string.Empty;
    public int UserType { get; set; } = 0; // 0: User, 1: Admin
    public string LabRoomId { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public bool IsAdmin { get => UserType == 1; }
    public bool IsUserAtOwnEditor { get; set; } = true;
    public string AddedToGroup { get; set; }
}
