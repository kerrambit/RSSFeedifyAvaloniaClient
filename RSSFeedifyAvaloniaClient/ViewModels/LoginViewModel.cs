using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSSFeedifyAvaloniaClient.Business.Errors;
using RSSFeedifyAvaloniaClient.Services.API.Auth;
using RSSFeedifyAvaloniaClient.Services.Validation;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

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

    public LoginViewModel() { }

    [RelayCommand]
    private async Task Login()
    {
        LoginActivated = true;

        LoginDTO loginData = new LoginDTO();
        loginData.Email = Email;
        loginData.Password = Password;
        loginData.RememberMe = false;

        var loginResult = await new LoginService().Login(loginData, _mainViewModel.HttpService, _mainViewModel.UriResourceCreator);
        if (loginResult.IsError)
        {
            string details = loginResult.GetError.Details;
            if (loginResult.GetError is HttpResponseMessageValidationError error)
            {
                details = error.ContentMessage;
            }

            OnUnsuccessfullLogin(details);
            return;
        }

        _mainViewModel.UserJWT = loginResult.GetValue.JWT;
        _mainViewModel.CurrentPage = new UserMainDashboardViewModel(_mainViewModel);

        LoginActivated = false;
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        _mainViewModel.CurrentPage = new RegisterViewModel(_mainViewModel);
    }

    private void OnUnsuccessfullLogin(string? details = null)
    {
        LoginError = $"Login was not successful! {(string.IsNullOrEmpty(details) ? "" : $"{details}")}";
        Email = string.Empty;
        EmailError = string.Empty;
        Password = string.Empty;
        PasswordError = string.Empty;
        LoginActivated = false;
    }
}
