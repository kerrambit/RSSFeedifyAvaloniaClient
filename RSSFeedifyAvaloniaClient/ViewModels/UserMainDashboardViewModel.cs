namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class UserMainDashboardViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public UserMainDashboardViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public string? JWT => _mainViewModel.UserJWT;

}
