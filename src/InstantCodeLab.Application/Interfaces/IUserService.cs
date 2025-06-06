using InstantCodeLab.Application.DTOs;

namespace InstantCodeLab.Application.Interfaces;

public interface IUserService
{
    UserDto JoinUser(LabLoginDto dto, string labId);
}
