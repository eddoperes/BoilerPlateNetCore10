using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public class Enterprise: Entity
    {

        public static readonly string InvalidIdErrorMessage = "Invalid id. Smaller than zero.";
        public static readonly string InvalidNameErrorMessage = "Invalid name. Name must be informed.";

        /*
        private Enterprise()
        {
        }
        */

        public Enterprise(string name)
        {
            Validate(name);
            Name = name;
            //CNPJ = cnpj;
            //Email = email;
            //Phone = phone;
            //Address = address;
        }

        public Enterprise(long id, string name)
        {
            DomainExceptionValidation.When(id < 0, InvalidIdErrorMessage);
            Id = id;

            Validate(name);
            Name = name;
            //CNPJ = cnpj;
            //Email = email;
            //Phone = phone;
            //Address = address;
        }

        private void Validate(string name)
        {
            DomainExceptionValidation.When(name == string.Empty, InvalidNameErrorMessage);
        }

        public string Name { get; protected set; } = "";

        //public CNPJ CNPJ { get; protected set; } = new CNPJ("");

        //public Email Email { get; protected set; } = new Email("");

        //public Phone Phone { get; protected set; } = new Phone("");

        //public Address Address { get; protected set; } = new Address("", 0, "", "", "", "", "");

    }
}
