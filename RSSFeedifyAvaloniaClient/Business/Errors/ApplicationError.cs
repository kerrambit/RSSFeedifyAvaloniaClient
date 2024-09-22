using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RSSFeedifyAvaloniaClient.Business.Errors
{
    public class ApplicationError
    {
        public string Message { get; }
        public string Details { get; }
        public string FileName { get; }
        public string MethodName { get; }
        public int LineNumber { get; }

        public IReadOnlyList<ApplicationError> InnerErrors => _innerErrors.AsReadOnly();

        private readonly List<ApplicationError> _innerErrors = new List<ApplicationError>();

        public ApplicationError(string message, string details,
            [CallerFilePath] string fileName = "",
            [CallerMemberName] string methodName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Message = message;
            Details = details;
            FileName = fileName;
            MethodName = methodName;
            LineNumber = lineNumber;
        }

        public ApplicationError(string message)
            : this(message, string.Empty) { }

        public ApplicationError(IExternalError externalError)
            : this(externalError.Message, externalError.Details) { }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            FormatError(stringBuilder, this, 0);
            return stringBuilder.ToString();
        }

        private static void FormatError(StringBuilder builder, ApplicationError error, int level)
        {
            builder.AppendLine($"{new string(' ', level * 4)}[{error.GetType().Name}]: {error.Message}. Details: '{error.Details}'.");

            foreach (var innerError in error.InnerErrors)
            {
                FormatError(builder, innerError, level + 1);
            }
        }
    }
}
