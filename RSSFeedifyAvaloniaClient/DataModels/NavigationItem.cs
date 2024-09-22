using System.Collections.ObjectModel;

namespace RSSFeedifyAvaloniaClient.DataModels
{
    public class NavigationItem
    {
        public string Tag { get; set; }
        public string Content { get; set; }
        public string IconSource { get; set; }
        public ObservableCollection<NavigationItem> SubItems { get; } = new ObservableCollection<NavigationItem>();

        public NavigationItem(string tag, string content, string iconSource)
        {
            Tag = tag;
            Content = content;
            IconSource = iconSource;
        }
    }
}
