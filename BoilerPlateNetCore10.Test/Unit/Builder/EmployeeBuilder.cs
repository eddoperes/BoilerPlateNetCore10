using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.ValueObjects;

namespace BoilerPlateNetCore10.Test.Unit.Builder
{

    public class EmployeeBuilder
    {

        private long _id;
        private string _name;
        private CPF _cpf;
        private Email _email;
        private Phone _phone;
        private Address _address;
        private DateTime _admission;
        private DateTime _resignation;
        private long _employerId;

        private EmployeeBuilder()
        {
            _name = "Nome de teste";
            _cpf = new CPF("00000000272");
            _email = new Email("email@gmail.com");
            _phone = new Phone("11912345678");
            _address = new Address("Rua de teste", 123, "", "Bairro de teste", "12345678", "Cidade de teste", "SP");
            _admission = new DateTime(2020, 1, 1);
            _resignation = new DateTime(2025, 1, 1);            
            _employerId = 1;
        }

        public static EmployeeBuilder GetEmployeeBuilder()
        {
            return new EmployeeBuilder();
        }

        public EmployeeBuilder ApplyName(string name)
        {
            _name = name;
            return this;
        }

        public EmployeeBuilder ApplyCPF(CPF cpf)
        {
            _cpf = cpf;
            return this;
        }

        public EmployeeBuilder ApplyEmail(Email email)
        {
            _email = email;
            return this;
        }

        public EmployeeBuilder ApplyPhone(Phone phone)
        {
            _phone = phone;
            return this;
        }

        public EmployeeBuilder ApplyAddress(Address address)
        {
            _address = address;
            return this;
        }        
        public EmployeeBuilder ApplyAdmission(DateTime admission)
        {
            _admission = admission;
            return this;
        }

        public EmployeeBuilder ApplyResignation(DateTime resignation)
        {
            _resignation = resignation;
            return this;
        }

        public EmployeeBuilder ApplyEmployerId(long employerId)
        {
            _employerId = employerId;
            return this;
        }

        public EmployeeBuilder ApplyId(long id)
        {
            _id = id;
            return this;
        }       

        public Employee Build()
        {
            var employee = new Employee(_id,
                                        _name,
                                        _cpf,
                                        _email,
                                        _phone,
                                        _address,
                                        _admission,
                                        _resignation,
                                        _employerId);
            return employee;
        }

    }
    
}
