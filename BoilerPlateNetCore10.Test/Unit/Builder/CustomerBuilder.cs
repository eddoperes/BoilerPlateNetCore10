using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.ValueObjects;

namespace BoilerPlateNetCore10.Test.Unit.Builder
{
    public class CustomerBuilder
    {

        private long _id;
        private string _name;
        private CPF _cpf;
        private Email _email;
        private Phone _phone;
        private Address _address;
        private DateTime _since;
        private long _supplierId;

        private CustomerBuilder()
        {
            _name = "Nome de teste";
            _cpf = new CPF("00000000272");
            _email = new Email("email@gmail.com");
            _phone = new Phone("11912345678");
            _address = new Address("Rua de teste", 123, "", "Bairro de teste", "12345678", "Cidade de teste", "SP");
            _since = new DateTime(2020, 1, 1);
            _supplierId = 1;
        }

        public static CustomerBuilder GetCustomerBuilder()
        {
            return new CustomerBuilder();
        }

        public CustomerBuilder ApplyName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerBuilder ApplyCPF(CPF cpf)
        {
            _cpf = cpf;
            return this;
        }

        public CustomerBuilder ApplyEmail(Email email)
        {
            _email = email;
            return this;
        }

        public CustomerBuilder ApplyPhone(Phone phone)
        {
            _phone = phone;
            return this;
        }

        public CustomerBuilder ApplyAddress(Address address)
        {
            _address = address;
            return this;
        }

        public CustomerBuilder ApplySince(DateTime since)
        {
            _since = since;
            return this;
        }

        public CustomerBuilder ApplySupplierId(long supplierId)
        {
            _supplierId = supplierId;
            return this;
        }

        public CustomerBuilder ApplyId(long id)
        {
            _id = id;
            return this;
        }

        public Customer Build()
        {
            var customer = new Customer(_id,
                                        _name,
                                        _cpf,
                                        _email,
                                        _phone,
                                        _address, 
                                        _since, 
                                        _supplierId);
            return customer;
        }

    }
}
