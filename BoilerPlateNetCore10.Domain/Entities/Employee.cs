using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System;

namespace BoilerPlateNetCore10.Domain.Entities
{
    
    public class Employee : Person
    {
        
        public static readonly string InvalidResignationErrorMessage = "Invalid resignation. Resignation must be after admission.";

        public Employee() {
        }

        public Employee(string name, CPF cpf, Email email, Phone phone, Address address, DateTime admission, DateTime? resignation, long employerId) : base(name, cpf, email, phone, address)
        {
            Validate(admission, resignation);
            Admission = admission;
            Resignation = resignation;
            EmployerId = employerId;
        }

        public Employee(long id, string name, CPF cpf, Email email, Phone phone, Address address, DateTime admission, DateTime? resignation, long employerId) : base(id, name, cpf, email, phone, address)
        {
            Validate(admission, resignation);
            Admission = admission;
            Resignation = resignation;
            EmployerId = employerId;
        }

        private void Validate(DateTime admission, DateTime? resignation)
        {
            if (resignation != null)            
                DomainExceptionValidation.When(resignation > admission, InvalidResignationErrorMessage);                        
        }

        public DateTime Admission { get; private set; }

        public DateTime? Resignation { get; private set; }

        public long EmployerId { get; private set; }

        public Enterprise? Employer { get; private set; }

    }
    
}
