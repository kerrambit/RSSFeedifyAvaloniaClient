namespace RSSFeedifyAvaloniaClient.Business.Errors
{
    public class ApplicationErrorAdapter : IExternalError
    {
        public string Message { get; }
        public string Details { get; }

        public ApplicationErrorAdapter(ClientNetLib.Business.Errors.Error error)
        {
            Message = error.ToString();
            Details = string.Empty;
        }

        public ApplicationErrorAdapter(ClientNetLib.Business.Errors.DetailedError error)
        {
            Message = error.Error.ToString();
            Details = error.Details;
        }
    }
}
