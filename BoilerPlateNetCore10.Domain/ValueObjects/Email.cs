using BoilerPlateNetCore10.Domain.Validation;
using System.Text.RegularExpressions;

namespace BoilerPlateNetCore10.Domain.ValueObjects
{
    public class Email
    {
        public static readonly string InvalidEmailErrorMessage = "Invalid Email. Email is not well formed.";

        public static bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Regex simples para validar formato básico (pode ser mais complexa)
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
      
        public Email(string address)
        {
            DomainExceptionValidation.When(!IsEmail(address), InvalidEmailErrorMessage);

            Address = address;
        }

        public string Address { get; private set; }

    }
}
