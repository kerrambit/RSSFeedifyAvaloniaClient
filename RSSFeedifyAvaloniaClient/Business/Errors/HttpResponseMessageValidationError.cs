using System.Text;

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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            FormatError(stringBuilder, this, 0);
            return stringBuilder.ToString();
        }

        private static void FormatError(StringBuilder builder, HttpResponseMessageValidationError error, int level)
        {
            builder.AppendLine($"{new string(' ', level * 4)}[{error.GetType().Name}]: {error.Message}. Details: '{error.Details}'. HTTP response content: '{error.ContentMessage}'.");

            foreach (var innerError in error.InnerErrors)
            {
                ApplicationError.FormatError(builder, innerError, level + 1);
            }
        }
    }
}
