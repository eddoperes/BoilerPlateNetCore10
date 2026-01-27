using BoilerPlateNetCore10.Domain.Validation;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoilerPlateNetCore10.Domain.ValueObjects
{
    public class Address
    {

        public static readonly string EmptyStreetErrorMessage = "Invalid street. Street must be informed.";
        public static readonly string NumberNotGreaterThanZeroErrorMessage = "Invalid number. Number must be greater than zero.";
        public static readonly string EmptyNeighborhoodErrorMessage = "Invalid neighborhood. Neighborhood must be informed.";
        public static readonly string EmptyZipCodeErrorMessage = "Invalid zip code. Zip code must be informed.";
        public static readonly string ZipCodeNotNumericErrorMessage = "Invalid zip code. Zip code must be numeric.";
        public static readonly string ZipCodeLenghtErrorMessage = "Invalid zip code. Zip code must have eight characters.";
        public static readonly string EmptyCityErrorMessage = "Invalid city. City must be informed.";
        public static readonly string InvalidStateErrorMessage = "Invalid state. State must be a valid Brazilian state abbreviation.";

        private static readonly Dictionary<string, string> BrazilianStates = new Dictionary<string, string>{
            { "AC", "Acre" },
            { "AL", "Alagoas" },
            { "AP", "Amapá" },
            { "AM", "Amazonas" },
            { "BA", "Bahia"  },
            { "CE", "Ceará" },
            { "DF", "Distrito Federal" },
            { "ES", "Espírito Santo" },
            { "GO", "Goiás" },
            { "MA", "Maranhão" },
            { "MT", "Mato Grosso" },
            { "MS", "Mato Grosso do Sul" },
            { "MG", "Minas Gerais" },
            { "PA", "Pará" },
            { "PB", "Paraíba" },
            { "PR", "Paraná" },
            { "PE", "Pernambuco" },
            { "PI", "Piauí" },
            { "RJ", "Rio de Janeiro" },
            { "RN", "Rio Grande do Norte" },
            { "RS", "Rio Grande do Sul" },
            { "RO", "Rondônia" },
            { "RR", "Roraima" },
            { "SC", "Santa Catarina" },
            { "SE", "Sergipe" },
            { "SP", "São Paulo" },
            { "TO", "Tocantins" }
        };

        internal Address()
        {
        }

        public Address(string street, int number, string complement, string neighborhood, string zipCode, string city, string state)
        {
            DomainExceptionValidation.When(street == string.Empty, EmptyStreetErrorMessage);
            DomainExceptionValidation.When(number <= 0, NumberNotGreaterThanZeroErrorMessage);
            DomainExceptionValidation.When(neighborhood == string.Empty, EmptyNeighborhoodErrorMessage);
            DomainExceptionValidation.When(zipCode == string.Empty, EmptyZipCodeErrorMessage);
            DomainExceptionValidation.When(!long.TryParse(zipCode, out _), ZipCodeNotNumericErrorMessage);
            DomainExceptionValidation.When(zipCode.Length != 8, ZipCodeLenghtErrorMessage);
            DomainExceptionValidation.When(city == string.Empty, EmptyCityErrorMessage);

            /*
            if (!BrazilianStates.ContainsKey(state))
            {
                state = "state";
            }
            */

            DomainExceptionValidation.When(!BrazilianStates.ContainsKey(state), InvalidStateErrorMessage);

            

            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;            
        }

        public string Street { get; private set; } = "";
        public  int Number { get; private set; }
        public  string Complement { get; private set; } = "";
        public string Neighborhood { get; private set; } = "";
        public string ZipCode { get; private set; } = "";
        public string City { get; private set; } = "";
        public string State { get; private set; } = "";
    }
}
