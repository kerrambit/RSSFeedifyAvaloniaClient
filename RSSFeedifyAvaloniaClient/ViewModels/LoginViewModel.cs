using ClientNetLib.Services.Json;
using ClientNetLib.Services.Networking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    private readonly HttpResponseMessageValidator _httpResponseMessageValidator = new HttpResponseMessageValidatorBuilder()
            .AddStatusCodeCheck(HTTPService.StatusCode.OK)
            .AddContentTypeCheck(HTTPService.ContentType.AppJson)
            .Build();

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _emailError = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _passwordError = string.Empty;

    [ObservableProperty]
    private string _loginError = string.Empty;

    public LoginViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public LoginViewModel() {}

    [RelayCommand]
    private async Task Login()
    {
        LoginDTO loginData = new LoginDTO();
        loginData.Email = Email;
        loginData.Password = Password;
        loginData.RememberMe = false;

        var postResult = await _mainViewModel.HttpService.PostAsync(_mainViewModel.UriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "login"), JsonConvertor.ConvertObjectToJsonString(loginData));
        if (postResult.IsError)
        {
            LoginError = $"Login was not successful. Detailed message: '{postResult.GetError}'.";
            return;
        }

        var response = postResult.GetValue;
        var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
        if (validationResult.IsError)
        {
            LoginError = $"Login was not successful. Detailed message: '{validationResult.GetError}'.";
            return;
        }

        var jsonResult = await JsonFromHttpResponseReader.ReadJson<LoginResponseDTO>(response);
        if (jsonResult.IsError)
        {
            LoginError = $"Login was not successful. Detailed message: '{jsonResult.GetError}'.";
            return;
        }

        _mainViewModel.UserJWT = jsonResult.GetValue.JWT;
        _mainViewModel.CurrentPage = new UserMainDashboardViewModel(_mainViewModel);
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        _mainViewModel.CurrentPage = new RegisterViewModel(_mainViewModel);
    }
}
