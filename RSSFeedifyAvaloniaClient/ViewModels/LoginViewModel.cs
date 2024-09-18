using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

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
    private void Login()
    {
        if (Email == "email" && Password == "1234")
        {
            _mainViewModel.UserJWT = "4654g6re4g6wr4g9wr7g98wr7g97wer7g8w";
            _mainViewModel.CurrentPage = new UserMainDashboardViewModel(_mainViewModel);
        }
        else
        {
            EmailError = "Wrong email format";
            PasswordError = "Password length is too small";
            LoginError = "Unable to login";
        }
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        _mainViewModel.CurrentPage = new RegisterViewModel(_mainViewModel);
    }
}
