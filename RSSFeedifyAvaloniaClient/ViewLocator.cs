using Avalonia.Controls;
using Avalonia.Controls.Templates;
using RSSFeedifyAvaloniaClient.ViewModels;
using System;

namespace RSSFeedifyAvaloniaClient
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            if (param is null)
            {
                return new TextBlock { Text = $"Page not found." };
            }

            var name = param.GetType()?.FullName?.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type is null)
            {
                return new TextBlock { Text = $"Page '{name}' not found." };
            }

            return (Control?)Activator.CreateInstance(type);
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
