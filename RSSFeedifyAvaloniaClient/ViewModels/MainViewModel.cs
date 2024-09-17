using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RSSFeedifyAvaloniaClient.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "RSSFeedify GUI Client";

    [ObservableProperty]
    private int _counter = 0;

    [RelayCommand]
    private void ButtonClicked()
    {
        Counter++;
    }
}
