using System.Threading.Tasks;

namespace InstantCodeLab.Infrastructure.External.Interface;

public interface IApiService
{
   Task<string> SendPostRequest(string body);
}
