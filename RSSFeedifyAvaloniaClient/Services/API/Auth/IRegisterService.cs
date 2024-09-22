using ClientNetLib.Services.Networking;
using RSSFeedifyCommon.Models;
using RSSFeedifyCommon.Types;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API.Auth
{
    public interface IRegisterService
    {
        Task<Result<RegisterService.Unit, string>> Register(RegisterDTO registrationData, HTTPService httpService, UriResourceCreator uriResourceCreator);
    }
}