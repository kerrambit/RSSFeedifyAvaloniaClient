using RSSFeedifyAvaloniaClient.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSFeedifyAvaloniaClient.Services.API
{
    public interface IUserSpecifiedPopulatedRSSFeedCategoriesLoader
    {
        // TODO: the result type will be replaced by RSSFeedifyCommon type
        Task<IList<NavigationItem>> LoadAsync();
    }
}
