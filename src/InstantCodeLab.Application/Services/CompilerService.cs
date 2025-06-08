using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Application.Interfaces;
using InstantCodeLab.Domain.Constants;
using InstantCodeLab.Domain.Enums;
using InstantCodeLab.Infrastructure.External.Interface;

namespace InstantCodeLab.Application.Services;

public class CompilerService : ICompilerService
{
    private readonly IRemoteCompilerService _remoteCompilerService;

    public CompilerService(IRemoteCompilerService remoteCompilerService)
    {
        _remoteCompilerService = remoteCompilerService;
    }

    public async Task<CompileResponseDto> Compile(CompileResquestDto dto)
    {
        var languageVersion = Constants.LanguageVersions.TryGetValue(dto.Language, out int version);
        var output =  await _remoteCompilerService.SendPostRequest(dto.Code, new KeyValuePair<LanguageCode, int>(dto.Language, version), dto.StdinInput);

        return new CompileResponseDto() { Output = output };
    }
}

