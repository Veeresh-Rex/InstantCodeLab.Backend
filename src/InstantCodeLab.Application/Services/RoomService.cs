using System.Linq;
using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using InstantCodeLab.Infrastructure.Utilities;
using System.Threading.Tasks;

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

    public async Task<RoomResponseDto> CreateRoom(RoomRequestDto request)
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

        await _labRoomRepository.CreateAsync(labRoom);
        string forntendUrl = _configuration.GetValue<string>("FrontendUrl") ?? "https://instant-code-lab.vercel.app";

        return new RoomResponseDto
        {
            AdminUrl = forntendUrl + "/editor/admin/" + labRoom._id,
            MembersUrl = forntendUrl + "/editor/" + labRoom._id,
        };
    }

    public async Task<GetRoomResponseDto> GetRoom(string roomId)
    {
        var room = await _labRoomRepository.GetByIdAsync(roomId);

        if (room is null)
        {
            throw new NotFoundException("No room found");
        }

        return new GetRoomResponseDto()
        {
            Id = room._id.ToString(),
            LabName = room.RoomName,
            IsRoomPasswordEnabled = !string.IsNullOrWhiteSpace(room.Password),
            IsAdminPinEnabled = !string.IsNullOrEmpty(room.AdminPin),
            Language = room.LanguageCode
        };
    }
}
