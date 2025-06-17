using System;
using System.Collections.Generic;

namespace InstantCodeLab.Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = string.Empty;
    public string OwnCode { get; set; } = string.Empty;
    public int UserType { get; set; } = 0; // 0: User, 1: Admin
    public string LabRoomId { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsAdmin { get => UserType == 1; }
    public bool IsUserAtOwnEditor { get; set; } = true;
    public SortedSet<string> ViewerConnectionIds { get; set; } = new SortedSet<string>();
    public string ViewingOfConnectionId { get; set; }
}
