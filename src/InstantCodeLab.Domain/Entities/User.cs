using System;

namespace InstantCodeLab.Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserName { get; set; } = string.Empty;
    public string OwnCode { get; set; } = string.Empty;
    public int UserType { get; set; } = 0; // 0: User, 1: Admin
    public string LabRoomId { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public bool IsConnected { get; set; } = true;
    public string PairedWithConnectionId { get; set; } = string.Empty;
    public bool IsAtOwnIde { get => PairedWithConnectionId == ConnectionId; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
