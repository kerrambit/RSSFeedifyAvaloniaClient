using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using RSSFeedifyAvaloniaClient.ViewModels;
using System.Linq;

namespace RSSFeedifyAvaloniaClient.Views;

public partial class UserMainDashboardView : UserControl
{
    public UserMainDashboardView()
    {
        AvaloniaXamlLoader.Load(this);

        // Default NavView
        var nv = this.FindControl<NavigationView>("nvSample1");
        nv.SelectionChanged += OnNVSample1SelectionChanged;
        nv.SelectedItem = nv.MenuItems.ElementAt(0);

    }

    private void OnNVSample1SelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
    {
        //if (e.IsSettingsSelected)
        //{
        //    (sender as NavigationView).Content = new NVSamplePageSettings();
        //}
        //else if (e.SelectedItem is NavigationViewItem nvi)
        //{
        //    var smpPage = $"FAControlsGallery.Pages.NVSamplePages.NV{nvi.Tag}";
        //    var pg = Activator.CreateInstance(Type.GetType(smpPage));
        //    (sender as NavigationView).Content = pg;
        //}

        if (e.SelectedItem is NavigationViewItem navigationViewItem)
        {
            var tag = navigationViewItem.Tag;
            if (tag != null)
            {
                UserControl? page = tag switch
                {
                    "SamplePage1" => new LoginView(),
                    _ => null
                };

                if (page is not null)
                {
                    (sender as NavigationView).Content = page;
                    page.DataContext = new LoginViewModel();
                }
            }
        }
    }
}
