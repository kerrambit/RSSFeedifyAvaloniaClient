using ClientNetLib.Services.Networking;
using RSSFeedifyAvaloniaClient.Business.Errors;
using RSSFeedifyCommon.Models;
using RSSFeedifyCommon.Types;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API.Auth
{
    public interface IRegisterService
    {
        // TODO: RSSFeedifyCommon should offer result type where only error value is accesible
        Task<Result<RegisterService.Unit, ApplicationError>> Register(RegisterDTO registrationData, HTTPService httpService, UriResourceCreator uriResourceCreator);
    }
}