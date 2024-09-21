using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.ViewModels
{
    public partial class UserMainDashboardTabsViewModel : ViewModelBase
    {
        public ObservableCollection<DocumentItem> Documents { get; } = new ObservableCollection<DocumentItem>();

        public UserMainDashboardTabsViewModel()
        {
            Documents.Add(new DocumentItem("0"));
            Documents.Add(new DocumentItem("1"));
        }

        [RelayCommand]
        private void AddDocument(object obj)
        {
            Documents.Add(new DocumentItem($"{Documents.Count + 1}"));
        }

        [RelayCommand]
        private void CloseTab(object obj)
        {
            Documents.Remove((DocumentItem)obj);
        }
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
            // Check if the other object is null
            if (other is null) return false;

            // Check if the headers are equal
            return string.Equals(Header, other.Header, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            // If the object is not a DocumentItem, return false
            if (obj is not DocumentItem other) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Header?.GetHashCode() ?? 0; // Use null-coalescing to handle null header
        }
    }
}

