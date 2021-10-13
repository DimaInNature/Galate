using System.Text.RegularExpressions;

namespace Galate.Services
{
    public sealed class ValidationService
    {
        public static bool MailIsValid(string mail,
            string filter = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)") => Regex.IsMatch(mail, filter); 
    }
}
