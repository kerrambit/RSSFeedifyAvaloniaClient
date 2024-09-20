using ClientNetLib.Services.Networking;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public HTTPService HttpService { get; }
    public UriResourceCreator UriResourceCreator { get; } = new UriResourceCreator(new(@"http://localhost:32000/api/"));

    public MainViewModel(HTTPService httpService)
    {
        this.HttpService = httpService;
        // Application may remember the last user (name & password) and will login automatically.
        //if (_loggedIn)
        //{
        //    CurrentPage = new UserMainDashboardViewModel(this);
        //    return;
        //}

        CurrentPage = new LoginViewModel(this);
    }
}
