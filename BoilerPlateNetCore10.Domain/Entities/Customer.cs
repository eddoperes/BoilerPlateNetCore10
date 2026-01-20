using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System;
using System.Net;
using System.Numerics;

namespace BoilerPlateNetCore10.Domain.Entities
{
    /*
    public class Customer : Person
    {

        public static readonly string InvalidSinceErrorMessage = "Invalid since. Since can not be a future date";

        private Customer() { 
        }

        public Customer(string name, CPF cpf, Email email, Phone phone, Address address, DateTime since, long enterpriseId) : base(name, cpf, email, phone, address)
        {
            Validate(since);
            Since = since;
            EnterpriseId = enterpriseId;
        }

        public Customer(long id, string name, CPF cpf, Email email, Phone phone, Address address, DateTime since, long enterpriseId) : base(id, name, cpf, email, phone, address)
        {
            Validate(since);
            Since = since;
            EnterpriseId = enterpriseId;
        }

        private void Validate(DateTime since)
        {         
            DomainExceptionValidation.When(since > DateTime.Now, InvalidSinceErrorMessage);
        }

        public DateTime Since { get; private set; }

        public long EnterpriseId { get; private set; }

        public Enterprise? Enterprise { get; private set; }

    }
    */
}
