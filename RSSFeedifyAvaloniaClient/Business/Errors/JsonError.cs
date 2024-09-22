namespace RSSFeedifyAvaloniaClient.Business.Errors
{
    public class JsonError : ApplicationError
    {
        public JsonError(string details)
            : base("error occured when processing JSON format", details) { }

        public JsonError(IExternalError externalError)
            : base(externalError) { }
    }
}
