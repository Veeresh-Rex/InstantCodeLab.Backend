using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace InstantCodeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("{labId}")]
    public ActionResult JoinUser([FromBody] LabLoginDto dto, [FromRoute] string labId)
    {
        if (dto == null)
        {
            return BadRequest();
        }
        var user = _userService.JoinUser(dto, labId);

        return Ok(user);
    }
}
