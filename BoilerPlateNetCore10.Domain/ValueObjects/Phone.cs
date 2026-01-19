using BoilerPlateNetCore10.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace BoilerPlateNetCore10.Domain.ValueObjects
{

    public class Phone
    {
        
        public static string EmptyNumberErrorMessage = "Invalid phone number. Phone number must be informed.";
        public static string NumberNotNumericErrorMessage = "Invalid phone number. Phone number must be numeric.";
        public static string NumberLenghtErrorMessage = "Invalid phone number. Phone number must have ten or eleven characters.";
        public static string InvalidDDDErrorMessage = "Invalid DDD. DDD must between 11 and 99.";
        public static string InvalidCellPhoneNumberErrorMessage = "Invalid cell phone number. Cell phone number must start with 9.";

        public Phone(string number) {
            DomainExceptionValidation.When(number == string.Empty, EmptyNumberErrorMessage);
            DomainExceptionValidation.When(!int.TryParse(number, out _), NumberNotNumericErrorMessage);
            DomainExceptionValidation.When(number.Length != 10 && number.Length != 11, NumberLenghtErrorMessage);
            DomainExceptionValidation.When(int.Parse(number.Substring(0, 2)) < 11 || int.Parse(number.Substring(0, 2)) > 99, InvalidDDDErrorMessage);
            DomainExceptionValidation.When(number.Length == 11 && number[2] != '9', InvalidCellPhoneNumberErrorMessage);

            Number = number;
        }


        public string Number { get; private set; }

    }

}
