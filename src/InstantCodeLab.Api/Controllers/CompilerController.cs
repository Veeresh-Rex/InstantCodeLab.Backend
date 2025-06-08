using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstantCodeLab.Api.Controllers;

public class CompilerController : ControllerBase
{
    private readonly ICompilerService _compilerService;

    public CompilerController(ICompilerService compilerService)
    {
        _compilerService = compilerService;
    }

    [HttpPost]
    public ActionResult<CompileResponseDto> Compile([FromBody] CompileResquestDto dto)
    {
        var result = _compilerService.Compile(dto);

        return Ok(result);
    }
}