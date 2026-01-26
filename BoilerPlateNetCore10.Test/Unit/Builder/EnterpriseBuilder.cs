using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.ValueObjects;

namespace BoilerPlateNetCore10.Test.Unit.Builder
{
    public class EnterpriseBuilder
    {

        private long _id;
        private string _name;
        private CNPJ _cnpj;
        private Email _email;
        private Phone _phone;
        private Address _address;

        private EnterpriseBuilder()
        {
            _name = "Nome de teste";
            _cnpj = new CNPJ("02232873000155");
            _email = new Email("email@gmail.com");
            _phone = new Phone("11912345678");
            _address = new Address("Rua de teste", 123, "", "Bairro de teste", "12345678", "Cidade de teste", "SP");
        }

        public static EnterpriseBuilder GetEnterpriseBuilder()
        {
            return new EnterpriseBuilder();
        }

        public EnterpriseBuilder ApplyName(string name)
        {
            _name = name;
            return this; 
        }

        public EnterpriseBuilder ApplyCNPJ(CNPJ cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public EnterpriseBuilder ApplyEmail(Email email)
        {
            _email = email;
            return this;
        }

        public EnterpriseBuilder ApplyPhone(Phone phone)
        {
            _phone = phone;
            return this;
        }

        public EnterpriseBuilder ApplyAddress(Address address)
        {
            _address = address;
            return this;
        }

        public EnterpriseBuilder ApplyId(long id)
        {
            _id = id;
            return this;
        }

        public Enterprise Build() 
        { 
            var enterprise = new Enterprise(_id, 
                                            _name,
                                            _cnpj,
                                            _email,
                                            _phone,
                                            _address);            
            return enterprise;
        }

    }
}
