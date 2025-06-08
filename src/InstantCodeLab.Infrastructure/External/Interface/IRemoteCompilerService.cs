using System.Collections.Generic;
using System.Threading.Tasks;
using InstantCodeLab.Domain.Enums;

namespace InstantCodeLab.Infrastructure.External.Interface;

public interface IRemoteCompilerService
{
    Task<string> SendPostRequest(string code, KeyValuePair<LanguageCode, int> languageVersion, string input);
}
