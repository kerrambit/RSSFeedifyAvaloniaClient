using ClientNetLib.Services.Json;
using ClientNetLib.Services.Networking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSSFeedifyAvaloniaClient.Services.Validation;
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

    private bool _emailValidation = false;

    partial void OnEmailChanged(string value)
    {
        if (EmailValidator.Validate(in value, out _))
        {
            EmailError = string.Empty;
            _emailValidation = true;
        }
        else
        {
            EmailError = "Invalid email format!";
            _emailValidation = false;
        }

        LoginEnabled = _emailValidation && _passwordValidation;
    }

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _passwordError = string.Empty;

    private bool _passwordValidation = false;

    partial void OnPasswordChanged(string value)
    {
        if (value.Length < 8 || value.Length > 100)
        {
            PasswordError = "Password length is invalid! Password length must be at least 8 and at most 100 characters long.";
            _passwordValidation = false;
        }
        else
        {
            PasswordError = string.Empty;
            _passwordValidation = true;
        }

        LoginEnabled = _emailValidation && _passwordValidation;
    }

    [ObservableProperty]
    private bool _loginEnabled = false;

    [ObservableProperty]
    private string _loginError = string.Empty;

    [ObservableProperty]
    private bool _loginActivated = false;

    public LoginViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public LoginViewModel() {}

    [RelayCommand]
    private async Task Login()
    {
        LoginActivated = true;

        LoginDTO loginData = new LoginDTO();
        loginData.Email = Email;
        loginData.Password = Password;
        loginData.RememberMe = false;

        var postResult = await _mainViewModel.HttpService.PostAsync(_mainViewModel.UriResourceCreator.BuildUri(EndPoint.ApplicationUser.ConvertToString(), "login"), JsonConvertor.ConvertObjectToJsonString(loginData));
        if (postResult.IsError)
        {
            OnUnsuccessfullLogin();
            return;
        }

        var response = postResult.GetValue;
        var validationResult = _httpResponseMessageValidator.Validate(new HTTPService.HttpServiceResponseMessageMetaData(HTTPService.RetrieveContentType(response), HTTPService.RetrieveStatusCode(response)));
        if (validationResult.IsError)
        {
            OnUnsuccessfullLogin();
            return;
        }

        var jsonResult = await JsonFromHttpResponseReader.ReadJson<LoginResponseDTO>(response);
        if (jsonResult.IsError)
        {
            OnUnsuccessfullLogin();
            return;
        }

        _mainViewModel.UserJWT = jsonResult.GetValue.JWT;
        _mainViewModel.CurrentPage = new UserMainDashboardViewModel(_mainViewModel);

        LoginActivated = false;
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        _mainViewModel.CurrentPage = new RegisterViewModel(_mainViewModel);
    }

    private void OnUnsuccessfullLogin()
    {
        LoginError = $"Login was not successful!";
        Email = string.Empty;
        EmailError = string.Empty;
        Password = string.Empty;
        PasswordError = string.Empty;
        LoginActivated = false;
    }
}
