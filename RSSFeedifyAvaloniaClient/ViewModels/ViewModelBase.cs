using CommunityToolkit.Mvvm.ComponentModel;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    protected ViewModelBase _currentPage;
}
