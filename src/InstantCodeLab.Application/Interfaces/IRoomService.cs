using InstantCodeLab.Application.DTOs;

namespace InstantCodeLab.Application.Interfaces;

public interface IRoomService
{
   RoomResponseDto CreateRoom(RoomRequestDto request);
   GetRoomResponseDto GetRoom(string roomId);
}
