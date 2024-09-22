using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RSSFeedifyAvaloniaClient.ViewModels;
using System.Collections;
using System.Linq;

namespace RSSFeedifyAvaloniaClient.Views;

public partial class UserMainDashboardTabsView : UserControl
{
    public UserMainDashboardTabsView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TabView_TabCloseRequested(FluentAvalonia.UI.Controls.TabView sender, FluentAvalonia.UI.Controls.TabViewTabCloseRequestedEventArgs args)
    {
        var tabItems = sender.TabItems as IList;

        var headerToRemove = args.Tab.Header as string;
        if (tabItems != null)
        {
            var itemToRemove = tabItems
                .OfType<DocumentItem>()
                .FirstOrDefault(doc => doc.Header == headerToRemove);

            if (itemToRemove != null)
            {
                tabItems.Remove(itemToRemove);
            }
        }
    }
}