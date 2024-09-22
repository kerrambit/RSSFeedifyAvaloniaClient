using ClientNetLib.Services.Networking;
using RSSFeedifyAvaloniaClient.Business.Errors;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API.Auth
{
    public interface ILoginService
    {
        Task<RSSFeedifyCommon.Types.Result<LoginResponseDTO, ApplicationError>> Login(LoginDTO loginData, HTTPService httpService, UriResourceCreator uriResourceCreator);
    }
}
