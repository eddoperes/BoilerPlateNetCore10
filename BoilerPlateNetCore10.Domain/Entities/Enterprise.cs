using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public class Enterprise: Entity
    {

        public static readonly string InvalidIdErrorMessage = "Invalid id. Smaller than zero.";
        public static readonly string InvalidNameErrorMessage = "Invalid name. Name must be informed.";
                
        private Enterprise()
        {
            Name = "";
            CNPJ = new CNPJ("");
            Email = new Email("");
            Phone = new Phone("");
            Address = new Address("", 0, "", "", "", "", "");
        }         

        public Enterprise(string name, CNPJ cnpj, Email email, Phone phone, Address address)
        {
            Validate(name);
            Name = name;
            CNPJ = cnpj;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public Enterprise(long id, string name, CNPJ cnpj, Email email, Phone phone, Address address)
        {
            DomainExceptionValidation.When(id < 0, InvalidIdErrorMessage);
            Id = id;

            Validate(name);
            Name = name;
            CNPJ = cnpj;
            Email = email;
            Phone = phone;
            Address = address;
        }

        private void Validate(string name)
        {
            DomainExceptionValidation.When(name == string.Empty, InvalidNameErrorMessage);
        }

        public string Name { get; protected set; }

        public CNPJ CNPJ { get; protected set; }

        public Email Email { get; protected set; }

        public Phone Phone { get; protected set; }

        public Address Address { get; protected set; }

    }
}
