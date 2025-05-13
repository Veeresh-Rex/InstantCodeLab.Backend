using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Repositories;
using InstantCodeLab.Domain.Entities;
using System;

namespace InstantCodeLab.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly string LabId = "Codelabe";
    private readonly string LabPassword = "12345";
    private readonly string LabAdminPin = "1234";

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public LabLoginResponseDto JoinUser(LabLoginDto dto, string labId)
    {
        // Check labid is valid
        // Check if user is already in the lab
        // Check if user is admin
        // Check if user password is valid
        //if (dto == null)
        //{
        //    throw new ArgumentNullException(nameof(dto));
        //}

        //if(dto.Password != LabPassword)
        //{
        //    throw new Exception("Invalid password");
        //}

        //if(dto.AdminPin != LabAdminPin)
        //{
        //    throw new Exception("Invalid admin pin");
        //}

        User user = new User()
        {
            UserName = dto.Username,
            LabRoomId = labId,
            UserType = dto.IsAdmin ? 1 : 0,
            IsConnected = false,
        };
        _userRepository.Data.Add(user);

        return new LabLoginResponseDto
        {
            Id = user.Id,
            Username = user.UserName,
            IsAdmin = true,
            Code = user.OwnCode,
            LabRoomName = user.LabRoomId
        };
    }

}
