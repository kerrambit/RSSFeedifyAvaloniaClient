using ClientNetLib.Services.Json;
using ClientNetLib.Services.Networking;
using RSSFeedifyAvaloniaClient.Business.Errors;
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

        public async Task<RSSFeedifyCommon.Types.Result<Unit, ApplicationError>> Register(RegisterDTO registrationData, HTTPService httpService, UriResourceCreator uriResourceCreator)
        {
            var postResult = await httpService.PostAsync(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register"), JsonConvertor.ConvertObjectToJsonString(registrationData));
            if (postResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<Unit, ApplicationError>(new ApiError(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register").ToString(), postResult.GetError));
            }

            var response = postResult.GetValue;
            var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
            if (validationResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<Unit, ApplicationError>(new HttpResponseMessageValidationError(new ApplicationErrorAdapter(validationResult.GetError), await HTTPService.RetrieveAndStringifyContent(response)));
            }

            return RSSFeedifyCommon.Types.Result.Ok<Unit, ApplicationError>(new Unit());
        }
    }
}
