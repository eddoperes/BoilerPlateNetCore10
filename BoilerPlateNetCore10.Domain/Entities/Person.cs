using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace BoilerPlateNetCore10.Domain.Entities
{
    
    public abstract class Person: Entity
    {

        public static readonly string InvalidIdErrorMessage = "Invalid id. Smaller than zero.";
        public static readonly string InvalidNameErrorMessage = "Invalid name. Name must be informed.";


        protected Person()
        {
            Name = "";
            CPF = new CPF("");
            Email = new Email("");
            Phone = new Phone("");
            Address = new Address("", 0, "", "", "", "", "");
        }

        public Person(string name, CPF cpf, Email email, Phone phone, Address address)
        {
            Validate(name, cpf);
            Name = name;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Address = address;  
        }

        public Person(long id, string name, CPF cpf, Email email, Phone phone, Address address)
        {
            DomainExceptionValidation.When(id < 0, InvalidIdErrorMessage);
            Id = id;

            Validate(name, cpf);
            Name = name;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Address = address;
        }

        private void Validate(string name, CPF cpf)
        {
            DomainExceptionValidation.When(name == string.Empty, InvalidNameErrorMessage);
        }

        public string Name { get; protected set; } 

        public CPF CPF { get; protected set; } 

        public Email Email { get; protected set; }

        public Phone Phone { get; protected set; }

        public Address Address { get; protected set; }

    }
    
}
