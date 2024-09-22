using CommunityToolkit.Mvvm.ComponentModel;
using RSSFeedifyAvaloniaClient.DataModels;
using RSSFeedifyAvaloniaClient.Services.API;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class UserMainDashboardViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    [ObservableProperty]
    private NavigationItem _navigationItem;
    public string? JWT => _mainViewModel.UserJWT;

    public ObservableCollection<NavigationItem> NavigationViewItems { get; } = new ObservableCollection<NavigationItem>();
    public UserMainDashboardViewModel() { }
    public UserMainDashboardViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        NavigationViewItems.Add(new("All Feeds", "All Feeds", "PageFilled"));
        NavigationViewItems.Add(new("Watched Feeds", "Watched Feeds", "PageFilled"));
        NavigationViewItems.Add(new("Recommended", "Recommended", "PageFilled"));

        NavigationItem = NavigationViewItems[0];
        _ = LoadUserSpecificNavigationViewItemsAsync(new MockedUserSpecifiedPopulatedRSSFeedCategoriesLoader());
    }

    // This is mocked method for now.
    // TODO: RSSFeedify API must offer endpoint to retrieve user's categories.
    public async Task LoadUserSpecificNavigationViewItemsAsync(IUserSpecifiedPopulatedRSSFeedCategoriesLoader loader)
    {
        var data = await loader.LoadAsync();

        foreach (var item in data)
        {
            if (item.SubItems.Count > 0)
            {
                item.IconSource = "LibraryFilled";
            }

            NavigationViewItems.Add(item);
        }
    }

}
