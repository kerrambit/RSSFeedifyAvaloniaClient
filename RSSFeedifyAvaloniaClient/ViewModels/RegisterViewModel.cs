using CommunityToolkit.Mvvm.Input;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public RegisterViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    [RelayCommand]
    private void SwitchToLogin()
    {
        _mainViewModel.CurrentPage = new LoginViewModel(_mainViewModel);
    }
}
