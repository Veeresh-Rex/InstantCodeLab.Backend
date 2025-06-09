using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCodeLab.Domain.Enums;
using InstantCodeLab.Infrastructure.External.Models;

namespace InstantCodeLab.Infrastructure.External.Interface;

public interface IRemoteCompilerService
{
    Task<Response> SendPostRequest(string code, KeyValuePair<LanguageCode, int> languageVersion, string input);
}
