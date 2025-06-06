using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Repositories;
using InstantCodeLab.Domain.Entities;
using System;
using System.Linq;

namespace InstantCodeLab.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILabRoomRepository _labRoomRepository;

    public UserService(IUserRepository userRepository, ILabRoomRepository labRoomRepository)
    {
        _userRepository = userRepository;
        _labRoomRepository = labRoomRepository;
    }

    public UserDto JoinUser(LabLoginDto dto, string labId)
    {
        // Check labid is valid
        // Check if user is already in the lab
        // Check if user is admin
        // Check if user password is valid

        var labRoom = _labRoomRepository.Data.FirstOrDefault(e => e.Id == labId);

        if (labRoom is null)
        {
            throw new NotFoundException("Lab room not found");
        }

        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        if (dto.Password != labRoom.Password)
        {
            throw new Exception("Invalid password");
        }

        if ( dto.IsAdmin && (dto.AdminPin != labRoom.AdminPin))
        {
            throw new Exception("Invalid admin pin");
        }

        User user = new User()
        {
            Username = dto.Username,
            LabRoomId = labId,
            UserType = dto.IsAdmin ? 1 : 0
        };

        _userRepository.Data.Add(user);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            IsAdmin = dto.IsAdmin,
            Code = user.OwnCode,
            JoinedLabRoomName = labRoom.RoomName,
            JoinedLabRoomId = labId
        };
    }
}
