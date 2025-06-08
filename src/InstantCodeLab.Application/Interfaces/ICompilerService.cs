using System.Threading.Tasks;
using InstantCodeLab.Application.DTOs;

namespace InstantCodeLab.Application.Interfaces;

public interface ICompilerService
{
    Task<CompileResponseDto> Compile(CompileResquestDto dto);
}
