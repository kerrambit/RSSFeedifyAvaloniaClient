using ClientNetLib.Services.Json;
using ClientNetLib.Services.Networking;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.Auth
{
    public static class LoginService
    {
        private static readonly HttpResponseMessageValidator _httpResponseMessageValidator = new HttpResponseMessageValidatorBuilder()
            .AddStatusCodeCheck(HTTPService.StatusCode.OK)
            .AddContentTypeCheck(HTTPService.ContentType.AppJson)
            .Build();

        // TODO: use specialized error type instead of string in the future
        public static async Task<RSSFeedifyCommon.Types.Result<LoginResponseDTO, string>> Login(LoginDTO loginData, HTTPService httpService, UriResourceCreator uriResourceCreator)
        {
            var postResult = await httpService.PostAsync(uriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "login"), JsonConvertor.ConvertObjectToJsonString(loginData));
            if (postResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<LoginResponseDTO, string>(postResult.GetError);
            }

            var response = postResult.GetValue;
            var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
            if (validationResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<LoginResponseDTO, string>(await HTTPService.RetrieveAndStringifyContent(response));
            }

            var jsonResult = await JsonFromHttpResponseReader.ReadJson<LoginResponseDTO>(response);
            if (jsonResult.IsError)
            {
                return RSSFeedifyCommon.Types.Result.Error<LoginResponseDTO, string>(validationResult.GetError.Details);
            }

            var loginResponseDTO = new LoginResponseDTO();
            loginResponseDTO.JWT = jsonResult.GetValue.JWT;
            return RSSFeedifyCommon.Types.Result.Ok<LoginResponseDTO, string>(loginResponseDTO);
        }
    }
}
