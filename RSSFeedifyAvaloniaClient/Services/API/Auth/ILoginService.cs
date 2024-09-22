using ClientNetLib.Services.Networking;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API.Auth
{
    public interface ILoginService
    {
        Task<RSSFeedifyCommon.Types.Result<LoginResponseDTO, string>> Login(LoginDTO loginData, HTTPService httpService, UriResourceCreator uriResourceCreator);
    }
}
