using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSSFeedifyAvaloniaClient.Services.Validation;
using RSSFeedifyCommon.Models;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class RegisterViewModel : ViewModelBase
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

        RegistrationEnabled = _emailValidation && _passwordValidation;
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

        RegistrationEnabled = _emailValidation && _passwordValidation && _confirmPasswordValidation;
    }

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private string _confirmPasswordError = string.Empty;

    private bool _confirmPasswordValidation = false;

    partial void OnConfirmPasswordChanged(string value)
    {
        if (value.Length < 8 || value.Length > 100)
        {
            ConfirmPasswordError = "Password length is invalid! Password length must be at least 8 and at most 100 characters long.";
            _confirmPasswordValidation = false;
        }
        else if (value != Password)
        {
            ConfirmPasswordError = "Confirm password must be the same as already entered password!";
            _confirmPasswordValidation = false;
        }
        else
        {
            ConfirmPasswordError = string.Empty;
            _confirmPasswordValidation = true;
        }

        RegistrationEnabled = _emailValidation && _passwordValidation && _confirmPasswordValidation;
    }

    [ObservableProperty]
    private bool _registrationEnabled = false;

    [ObservableProperty]
    private string _registrationError = string.Empty;

    [ObservableProperty]
    private bool _registrationActivated = false;

    public RegisterViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    [RelayCommand]
    private async Task Register()
    {
        RegistrationActivated = true;

        var registrationData = new RegisterDTO();
        registrationData.Email = Email;
        registrationData.Password = Password;
        registrationData.ConfirmPassword = ConfirmPassword;

        var registrationResult = await Services.Auth.RegisterService.Register(registrationData, _mainViewModel.HttpService, _mainViewModel.UriResourceCreator);
        if (registrationResult.IsError)
        {
            OnUnsuccessfullLogin(registrationResult.GetError);
            return;
        }

        RegistrationActivated = false;

        var loginData = new LoginDTO();
        loginData.Email = Email;
        loginData.Password = Password;
        loginData.RememberMe = false;

        var loginResult = await Services.Auth.LoginService.Login(loginData, _mainViewModel.HttpService, _mainViewModel.UriResourceCreator);
        if (loginResult.IsError)
        {
            OnUnsuccessfullLogin(loginResult.GetError);
            return;
        }

        _mainViewModel.UserJWT = loginResult.GetValue.JWT;
        _mainViewModel.CurrentPage = new UserMainDashboardViewModel(_mainViewModel);
    }

    [RelayCommand]
    private void SwitchToLogin()
    {
        _mainViewModel.CurrentPage = new LoginViewModel(_mainViewModel);
    }

    private void OnUnsuccessfullLogin(string? details = null)
    {
        RegistrationError = $"Registration was not successful! {(string.IsNullOrEmpty(details) ? "" : $"{details}")}";
        Email = string.Empty;
        EmailError = string.Empty;
        Password = string.Empty;
        PasswordError = string.Empty;
        ConfirmPassword = string.Empty;
        ConfirmPasswordError = string.Empty;
        RegistrationActivated = false;
    }
}
