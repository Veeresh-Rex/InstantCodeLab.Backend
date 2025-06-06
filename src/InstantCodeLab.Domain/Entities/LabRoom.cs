using System;

namespace InstantCodeLab.Domain.Entities;

public class LabRoom
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string RoomName { get; set; } = "InstantCodeLab";
    public string? Password { get; set; }
    public string? AdminPin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
