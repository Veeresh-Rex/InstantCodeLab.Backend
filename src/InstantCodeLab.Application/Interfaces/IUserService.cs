using InstantCodeLab.Application.DTOs;

namespace InstantCodeLab.Application.Interfaces;

public interface IUserService
{
    LabLoginResponseDto JoinUser(LabLoginDto dto, string labId);
}
