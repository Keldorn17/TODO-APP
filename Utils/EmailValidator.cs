using System.Text.RegularExpressions;

namespace TODO.Utils
{
    public static partial class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = EmailRegex();
                return regex.IsMatch(email);
            }
            catch (RegexParseException)
            {
                return false;
            }
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();
    }
}
