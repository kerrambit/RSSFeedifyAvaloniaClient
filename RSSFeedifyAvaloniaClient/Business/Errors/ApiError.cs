namespace RSSFeedifyAvaloniaClient.Business.Errors
{
    public class ApiError : ApplicationError
    {
        public ApiError(string endpoint, string details)
            : base($"network error occured when calling API endpoint {endpoint}", details)
        {
        }

        public ApiError(string endpoint)
            : base($"network error occured when calling API endpoint {endpoint}")
        {
        }
    }
}
