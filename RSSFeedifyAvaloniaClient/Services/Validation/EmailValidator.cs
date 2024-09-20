using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RSSFeedifyAvaloniaClient.Services.Validation
{
    public class EmailValidator
    {
        public static bool Validate(in string value, out string result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = string.Empty;
                return false;
            }

            try
            {
                result = Regex.Replace(value, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                result = string.Empty;
                return false;
            }
            catch (ArgumentException e)
            {
                result = string.Empty;
                return false;
            }

            try
            {
                return Regex.IsMatch(value,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                result = string.Empty;
                return false;
            }
        }
    }
}
