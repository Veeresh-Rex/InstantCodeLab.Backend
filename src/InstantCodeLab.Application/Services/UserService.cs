using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Repositories;
using InstantCodeLab.Domain.Entities;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using InstantCodeLab.Infrastructure.Utilities;
using System.Threading.Tasks;

namespace InstantCodeLab.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILabRoomRepository _labRoomRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, ILabRoomRepository labRoomRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _labRoomRepository = labRoomRepository;
        _configuration = configuration;
    }

    public async Task<UserDto> JoinUser(LabLoginDto dto, string labId)
    {
        // Check labid is valid
        // Check if user is already in the lab
        // Check if user is admin
        // Check if user password is valid

        var labRoom = await _labRoomRepository.GetByIdAsync(labId);
        string passwordSalt = _configuration.GetValue<string>("PasswordOptions:PasswordSalt") ?? string.Empty;
        string adminSalt = _configuration.GetValue<string>("PasswordOptions:AdminPinSalt") ?? string.Empty;


        if (labRoom is null)
        {
            throw new NotFoundException("Lab room not found");
        }

        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        if (!string.IsNullOrWhiteSpace(labRoom.Password) && !PasswordHasher.VerifyPassword(dto.Password, labRoom.Password, passwordSalt))
        {
            throw new Exception("Invalid password");
        }

        if (dto.IsAdmin && !string.IsNullOrWhiteSpace(labRoom.AdminPin) && !PasswordHasher.VerifyPassword(dto.AdminPin, labRoom.AdminPin, adminSalt))
        {
            throw new Exception("Invalid admin pin");
        }

        User user = new User()
        {
            Username = dto.Username,
            LabRoomId = labId,
            OwnCode = string.Empty,
            UserType = dto.IsAdmin ? 1 : 0
        };

        await _userRepository.CreateAsync(user);

        return new UserDto
        {
            Id = user._id,
            Username = user.Username,
            IsAdmin = dto.IsAdmin,
            Code = user.OwnCode,
            JoinedLabRoomName = labRoom.RoomName,
            JoinedLabRoomId = labId
        };
    }
}
