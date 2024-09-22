using ClientNetLib.Services.Json;
using ClientNetLib.Services.Networking;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API.Auth
{
    public class RegisterService : IRegisterService
    {
        public struct Unit { }

        private static readonly HttpResponseMessageValidator _httpResponseMessageValidator = new HttpResponseMessageValidatorBuilder()
            .AddStatusCodeCheck(HTTPService.StatusCode.OK)
            .AddContentTypeCheck(HTTPService.ContentType.TxtPlain)
            .Build();

        // TODO: use specialized error type instead of string in the future
        public async Task<RSSFeedifyCommon.Types.Result<Unit, string>> Register(RegisterDTO registrationData, HTTPService httpService, UriResourceCreator uriResourceCreator)
        {
            var postResult = await httpService.PostAsync(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register"), JsonConvertor.ConvertObjectToJsonString(registrationData));
            if (postResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<Unit, string>(postResult.GetError);
            }

            var response = postResult.GetValue;
            var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
            if (validationResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<Unit, string>(await HTTPService.RetrieveAndStringifyContent(response));
            }

            return RSSFeedifyCommon.Types.Result.Ok<Unit, string>(new Unit());
        }
    }
}
