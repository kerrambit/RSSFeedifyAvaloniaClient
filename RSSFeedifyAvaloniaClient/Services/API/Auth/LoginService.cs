using ClientNetLib.Services.Json;
using ClientNetLib.Services.Networking;
using RSSFeedifyAvaloniaClient.Business.Errors;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API.Auth
{
    public class LoginService : ILoginService
    {
        private static readonly HttpResponseMessageValidator _httpResponseMessageValidator = new HttpResponseMessageValidatorBuilder()
            .AddStatusCodeCheck(HTTPService.StatusCode.OK)
            .AddContentTypeCheck(HTTPService.ContentType.AppJson)
            .Build();

        public async Task<RSSFeedifyCommon.Types.Result<LoginResponseDTO, ApplicationError>> Login(LoginDTO loginData, HTTPService httpService, UriResourceCreator uriResourceCreator)
        {
            var postResult = await httpService.PostAsync(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "login"), JsonConvertor.ConvertObjectToJsonString(loginData));
            if (postResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<LoginResponseDTO, ApplicationError>(new ApiError(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "login").ToString(), postResult.GetError));
            }

            var response = postResult.GetValue;
            var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
            if (validationResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<LoginResponseDTO, ApplicationError>(new HttpResponseMessageValidationError(new ApplicationErrorAdapter(validationResult.GetError), await HTTPService.RetrieveAndStringifyContent(response)));
            }

            var jsonResult = await JsonFromHttpResponseReader.ReadJson<LoginResponseDTO>(response);
            if (jsonResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<LoginResponseDTO, ApplicationError>(new JsonError(new ApplicationErrorAdapter(jsonResult.GetError)));
            }

            var loginResponseDTO = new LoginResponseDTO();
            loginResponseDTO.JWT = jsonResult.GetValue.JWT;
            return RSSFeedifyCommon.Types.Result.Ok<LoginResponseDTO, ApplicationError>(loginResponseDTO);
        }
    }
}
