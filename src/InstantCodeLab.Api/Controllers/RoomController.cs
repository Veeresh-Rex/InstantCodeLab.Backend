using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace InstantCodeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<ActionResult> JoinUser([FromBody] RoomRequestDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }
        var newRoom = await _roomService.CreateRoom(dto);

        return Ok(newRoom);
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<GetRoomResponseDto>> GetRoom(string roomId)
    {
        var room = await _roomService.GetRoom(roomId);

        return Ok(room);
    }
}
