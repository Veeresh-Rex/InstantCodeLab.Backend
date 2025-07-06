using InstantCodeLab.Domain.Enums;

namespace InstantCodeLab.Domain.Entities;

public class LabRoom : BaseEntity
{
    public string RoomName { get; set; } = "InstantCodeLab";
    public string? Password { get; set; }
    public string? AdminPin { get; set; }
    public LanguageCode LanguageCode { get; set; } = LanguageCode.nodejs;
}
