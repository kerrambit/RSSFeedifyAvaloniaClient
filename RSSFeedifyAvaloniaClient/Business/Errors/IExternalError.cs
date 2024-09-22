namespace RSSFeedifyAvaloniaClient.Business.Errors
{
    public interface IExternalError
    {
        string Message { get; }
        string Details { get; }
    }
}
