using CommunityToolkit.Mvvm.ComponentModel;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    protected ViewModelBase _currentPage;

    [ObservableProperty]
    protected string? _userJWT = null;
}
