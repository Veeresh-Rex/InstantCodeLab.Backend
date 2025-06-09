using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstantCodeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompilerController : ControllerBase
{
    private readonly ICompilerService _compilerService;

    public CompilerController(ICompilerService compilerService)
    {
        _compilerService = compilerService;
    }

    [HttpPost]
    public async Task<ActionResult> Compile([FromBody] CompileResquestDto dto)
    {
        var result = await _compilerService.Compile(dto);

        return Ok(result);
    }
}