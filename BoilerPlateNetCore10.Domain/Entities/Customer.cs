using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System;

namespace BoilerPlateNetCore10.Domain.Entities
{
    
    public class Customer : Person
    {

        public static readonly string InvalidSinceErrorMessage = "Invalid since. Since can not be a future date";

        public Customer() { 
        }

        public Customer(string name, CPF cpf, Email email, Phone phone, Address address, DateTime since, long supplierId) : base(name, cpf, email, phone, address)
        {
            Validate(since);
            Since = since;
            SupplierId = supplierId;
        }

        public Customer(long id, string name, CPF cpf, Email email, Phone phone, Address address, DateTime since, long supplierId) : base(id, name, cpf, email, phone, address)
        {
            Validate(since);
            Since = since;
            SupplierId = supplierId;
        }

        private void Validate(DateTime since)
        {         
            DomainExceptionValidation.When(since > DateTime.Now, InvalidSinceErrorMessage);
        }

        public DateTime Since { get; private set; }

        public long SupplierId { get; private set; }

        public Enterprise? Supplier { get; private set; }

    }
    
}
