using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ClientNetLib.Services.EnvironmentUtils;
using ClientNetLib.Services.Networking;
using RSSFeedifyAvaloniaClient.ViewModels;
using RSSFeedifyAvaloniaClient.Views;
using RSSFeedifyCommon.Services;

namespace RSSFeedifyAvaloniaClient;

public partial class App : Application
{
    private HTTPService _httpService = new HTTPService(new System.Net.Http.HttpClient());

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // Add the ViewLocator programmatically
        DataTemplates.Add(new ViewLocator());

        // Load directory for loggingsettings.json file.
        var configFilesDirectoryResult = ConfigDirectoryService.GetConfigFilesDirectory();
        if (configFilesDirectoryResult.IsError)
        {
            return;
        }

        LoggingService loggerService = new LoggingService(configFilesDirectoryResult.GetValue);
        Logger.Initialize(loggerService);
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
