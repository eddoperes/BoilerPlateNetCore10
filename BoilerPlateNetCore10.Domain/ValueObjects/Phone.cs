using BoilerPlateNetCore10.Domain.Validation;

namespace BoilerPlateNetCore10.Domain.ValueObjects
{

    public class Phone
    {
        
        public static readonly string EmptyNumberErrorMessage = "Invalid phone number. Phone number must be informed.";
        public static readonly string NumberNotNumericErrorMessage = "Invalid phone number. Phone number must be numeric.";
        public static readonly string NumberLenghtErrorMessage = "Invalid phone number. Phone number must have ten or eleven characters.";
        public static readonly string InvalidDDDErrorMessage = "Invalid DDD. DDD must between 11 and 99.";
        public static readonly string InvalidCellPhoneNumberErrorMessage = "Invalid cell phone number. Cell phone number must start with 9.";

        public Phone() 
        { 
        }

        public Phone(string number) {
            DomainExceptionValidation.When(number == string.Empty, EmptyNumberErrorMessage);
            DomainExceptionValidation.When(!long.TryParse(number, out _), NumberNotNumericErrorMessage);
            DomainExceptionValidation.When(number.Length != 10 && number.Length != 11, NumberLenghtErrorMessage);

            /*
            if (int.Parse(number.Substring(0, 2)) < 11 || int.Parse(number.Substring(0, 2)) > 99) { 
                number= "11" + number.Substring(2);
            }
            */

            DomainExceptionValidation.When(int.Parse(number.Substring(0, 2)) < 11 || int.Parse(number.Substring(0, 2)) > 99, InvalidDDDErrorMessage);
            DomainExceptionValidation.When(number.Length == 11 && number[2] != '9', InvalidCellPhoneNumberErrorMessage);

            Number = number;
        }


        public string Number { get; private set; } = "";

    }

}
