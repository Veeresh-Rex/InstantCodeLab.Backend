using System.Threading.Tasks;
using InstantCodeLab.Application.DTOs;

namespace InstantCodeLab.Application.Interfaces;

public interface IRoomService
{
    Task<RoomResponseDto> CreateRoom(RoomRequestDto request);
    Task<GetRoomResponseDto> GetRoom(string roomId);
}
