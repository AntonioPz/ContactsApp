using System.Text.RegularExpressions;

namespace ContactsApp.Bussiness
{
    public class UserValidation
    {
        internal static Result ValidateCredentials(string email, string password)
        {
            Result result = new Result()
            {
                IsValid = true,
                Message = string.Empty
            };
            if (string.IsNullOrEmpty(email))
            {
                result.IsValid = false;
                result.Message = "¡Ingresa tu correo!";
            }
            else if (!ValidatePattern(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                result.IsValid = false;
                result.Message = "¡Ingresa un correro válido!";
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.IsValid = false;
                result.Message = "¡Ingresa tu contraseña!";
            }
            return result;
        }
        private static bool ValidatePattern(string text, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(text);
            return match.Success;
        }
    }
}
