using FluentAvalonia.UI.Controls;
using RSSFeedifyAvaloniaClient.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API
{
    public class MockedUserSpecifiedPopulatedRSSFeedCategoriesLoader : IUserSpecifiedPopulatedRSSFeedCategoriesLoader
    {
        public async Task<IList<NavigationItem>> LoadAsync()
        {
            await Task.Delay(1000);

            var categories = new List<NavigationItem>();
            var techCategory = new NavigationItem("Tech", "Tech", "PageFilled");
            categories.Add(techCategory);

            var youtube = new NavigationItem("Youtube", "Youtube", "PageFilled");
            var ai = new NavigationItem("OpenAI.com", "OpenAI.com", "PageFilled");
            var nvidia = new NavigationItem("www.https://nvidia.news.com", "www.https://nvidia.news.com", "PageFilled");
            techCategory.SubItems.Add(youtube);
            techCategory.SubItems.Add(ai);
            techCategory.SubItems.Add(nvidia);

            return categories;
        }
    }
}
