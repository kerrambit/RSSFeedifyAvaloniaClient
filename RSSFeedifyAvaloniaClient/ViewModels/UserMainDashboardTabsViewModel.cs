using System;
using System.Collections.ObjectModel;

namespace RSSFeedifyAvaloniaClient.ViewModels
{
    public partial class UserMainDashboardTabsViewModel : ViewModelBase
    {
        public ObservableCollection<DocumentItem> Documents { get; } = new ObservableCollection<DocumentItem>();

        public UserMainDashboardTabsViewModel() { }
    }

    public class DocumentItem
    {
        public string Header { get; set; }

        public DocumentItem(string header)
        {
            Header = header;
        }

        public bool Equals(DocumentItem? other)
        {
            if (other is null) return false;
            return string.Equals(Header, other.Header, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not DocumentItem other) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Header?.GetHashCode() ?? 0;
        }
    }
}

