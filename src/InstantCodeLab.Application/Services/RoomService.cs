using System.Linq;
using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using InstantCodeLab.Infrastructure.Utilities;

namespace InstantCodeLab.Application.Services;

public class RoomService : IRoomService
{
    private readonly ILabRoomRepository _labRoomRepository;
    private readonly IConfiguration _configuration;

    public RoomService(ILabRoomRepository labRoomRepository, IConfiguration configuration)
    {
        _labRoomRepository = labRoomRepository;
        _configuration = configuration;
    }

    public RoomResponseDto CreateRoom(RoomRequestDto request)
    {
        string passwordSalt = _configuration.GetValue<string>("PasswordOptions:PasswordSalt") ?? string.Empty;
        string adminSalt = _configuration.GetValue<string>("PasswordOptions:AdminPinSalt") ?? string.Empty;

        string? hashedPassword = string.IsNullOrWhiteSpace(request.Password) ? null : PasswordHasher.HashPassword(request.Password, passwordSalt);
        string? hashedAdminPin = string.IsNullOrWhiteSpace(request.AdminPin) ? null : PasswordHasher.HashPassword(request.AdminPin, adminSalt);

        LabRoom labRoom = new LabRoom
        {
            RoomName = request.LabName,
            Password = hashedPassword,
            AdminPin = hashedAdminPin
        };

        _labRoomRepository.Data.Add(labRoom);
        string forntendUrl = _configuration.GetValue<string>("FrontendUrl") ?? "https://instant-code-lab.vercel.app";

        return new RoomResponseDto
        {
            AdminUrl = forntendUrl + "/editor/admin/" + labRoom.Id,
            MembersUrl = forntendUrl + "/editor/" + labRoom.Id,
        };
    }

    public GetRoomResponseDto GetRoom(string roomId)
    {
        var room = _labRoomRepository.Data.FirstOrDefault(e => e.Id.ToString() == roomId);

        if (room is null)
        {
            throw new NotFoundException("No room found");
        }

        return new GetRoomResponseDto()
        {
            Id = room.Id.ToString(),
            LabName = room.RoomName,
            IsRoomPasswordEnabled = !string.IsNullOrWhiteSpace(room.Password),
            IsAdminPinEnabled = !string.IsNullOrEmpty(room.AdminPin),
            Language = room.LanguageCode
        };
    }
}
