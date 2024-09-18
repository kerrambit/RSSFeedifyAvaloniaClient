using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    
    public MainViewModel()
    {
        // Application may remember the last user (name & password) and will login automatically.
        //if (_loggedIn)
        //{
        //    CurrentPage = new UserMainDashboardViewModel(this);
        //    return;
        //}

        CurrentPage = new LoginViewModel(this);
    }
}
