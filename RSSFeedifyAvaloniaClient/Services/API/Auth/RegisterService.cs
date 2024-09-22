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
            Logger.LogInformation($"Post data {registrationData} to API endpoint {uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register")}.");

            var postResult = await httpService.PostAsync(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register"), JsonConvertor.ConvertObjectToJsonString(registrationData));
            if (postResult.IsError)
            {
                var error = new ApiError(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register").ToString(), postResult.GetError);
                Logger.LogError(error.ToString());
                return RSSFeedifyCommon.Types.Result.Error<Unit, ApplicationError>(error);
            }

            var response = postResult.GetValue;
            var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
            if (validationResult.IsError)
            {
                var error = new HttpResponseMessageValidationError(new ApplicationErrorAdapter(validationResult.GetError), await HTTPService.RetrieveAndStringifyContent(response));
                Logger.LogError(error.ToString());
                return RSSFeedifyCommon.Types.Result.Error<Unit, ApplicationError>(error);
            }

            Logger.LogInformation($"Post to API endpoint {uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "register")} was successful.");
            return RSSFeedifyCommon.Types.Result.Ok<Unit, ApplicationError>(new Unit());
        }
    }
}
