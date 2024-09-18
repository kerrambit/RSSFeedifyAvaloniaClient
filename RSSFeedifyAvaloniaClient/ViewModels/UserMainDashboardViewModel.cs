using CommunityToolkit.Mvvm.ComponentModel;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class UserMainDashboardViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    [ObservableProperty]
    private string _jWT = string.Empty;

    public UserMainDashboardViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }


    
}
