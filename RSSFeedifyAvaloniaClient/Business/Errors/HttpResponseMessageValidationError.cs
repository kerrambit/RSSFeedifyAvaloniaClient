namespace RSSFeedifyAvaloniaClient.Business.Errors
{
    public class HttpResponseMessageValidationError : ApplicationError
    {
        public string ContentMessage { get; } = string.Empty;

        public HttpResponseMessageValidationError(string details)
            : base("error occured when validating HTTPResponseMessage object", details) { }

        public HttpResponseMessageValidationError(IExternalError externalError)
            : base(externalError) { }

        public HttpResponseMessageValidationError(string details, string contentMessage)
            : base("error occured when validating HTTPResponseMessage object", details)
        {
            ContentMessage = contentMessage;
        }

        public HttpResponseMessageValidationError(IExternalError externalError, string contentMessage)
            : base(externalError)
        {
            ContentMessage = contentMessage;
        }
    }
}
