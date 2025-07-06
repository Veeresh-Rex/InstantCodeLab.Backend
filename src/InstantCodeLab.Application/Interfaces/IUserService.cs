using System.Threading.Tasks;
using InstantCodeLab.Application.DTOs;

namespace InstantCodeLab.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> JoinUser(LabLoginDto dto, string labId);
}
