using System.Linq;
using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;

namespace InstantCodeLab.Application.Services;

public class RoomService : IRoomService
{
    private readonly ILabRoomRepository _labRoomRepository;

    public RoomService(ILabRoomRepository labRoomRepository)
    {
        _labRoomRepository = labRoomRepository;
    }

    public RoomResponseDto CreateRoom(RoomRequestDto request)
    {
        LabRoom labRoom = new LabRoom
        {
            RoomName = request.LabName,
            Password = request.Password,
            AdminPin = request.AdminPin
        };

        _labRoomRepository.Data.Add(labRoom);

        return new RoomResponseDto
        {
            AdminUrl = "http://localhost:5173/editor/admin/" + labRoom.Id,
            MembersUrl = "http://localhost:5173/editor/" + labRoom.Id,
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
            IsAdminPinEnabled = !string.IsNullOrEmpty(room.AdminPin)
        };
    }
}
