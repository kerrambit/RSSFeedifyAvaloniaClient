using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ClientNetLib.Services.Networking;
using RSSFeedifyAvaloniaClient.ViewModels;
using RSSFeedifyAvaloniaClient.Views;

namespace RSSFeedifyAvaloniaClient;

public partial class App : Application
{
    private HTTPService _httpService = new HTTPService(new System.Net.Http.HttpClient());

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // Add the ViewLocator programmatically
        this.DataTemplates.Add(new ViewLocator());
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_httpService)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(_httpService)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
