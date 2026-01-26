using Bogus;
using Bogus.Extensions.Brazil;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using BoilerPlateNetCore10.Test.Unit.Builder;
using BoilerPlateNetCore10.Test.Util;
using ExpectedObjects;
using Xunit.Abstractions;

namespace BoilerPlateNetCore10.Test.Unit
{
    public class CustomerTest
    {

        private readonly Faker _faker;
        private readonly ITestOutputHelper _output;
        private readonly CustomerBuilder _customerBuilder = CustomerBuilder.GetCustomerBuilder();

        public CustomerTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Executing construtor");

            _faker = new Faker("pt_BR");
            _output.WriteLine(_faker.Random.Double().ToString());
            _output.WriteLine(_faker.Company.CompanyName());
            _output.WriteLine(_faker.Person.Email);
        }

        protected void Dispose()
        {
            _output.WriteLine("Executing dispose");
        }

        [Fact]
        public void MustCreateCustomer()
        {

            //Arrange
            string name = _faker.Person.FullName;
            CPF cpf = new CPF(_faker.Person.Cpf());
            Email email = new Email(_faker.Person.Email);
            Phone phone = new Phone(_faker.Person.Phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("+55",""));
            Address address = new Address(_faker.Address.StreetName(),
                                          _faker.Random.Int(100, 1000),
                                          _faker.Address.SecondaryAddress(),
                                          _faker.Address.StreetAddress(),
                                          _faker.Address.ZipCode().Replace("-", ""),
                                          _faker.Address.City(),
                                          _faker.Address.StateAbbr());
            DateTime since = _faker.Date.Past(5);
            long supplierId = _faker.Random.Long(1, 1000);

            var expectedCustomer = new
            {
                Name = name,
                CPF = cpf,
                Email = email,
                Phone = phone,
                Address = address,
                Since = since,
                SupplierId = supplierId
            };

            //Act 
            var customer = _customerBuilder.ApplyName(expectedCustomer.Name)
                                           .ApplyCPF(expectedCustomer.CPF)
                                           .ApplyEmail(expectedCustomer.Email)
                                           .ApplyPhone(expectedCustomer.Phone)
                                           .ApplyAddress(expectedCustomer.Address)
                                           .ApplySince(expectedCustomer.Since)
                                           .ApplySupplierId(expectedCustomer.SupplierId)
                                           .Build();

            //Assert
            expectedCustomer.ToExpectedObject().ShouldMatch(customer);

        }

        [Fact]
        public void ShouldNotCreateCustomerWithNegativeId()
        {
            long id = -1;
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _customerBuilder.ApplyId(id).Build();
            }).WithMessage(Customer.InvalidIdErrorMessage);
        }

        [Fact]
        public void ShouldNotCreateCustomerWithEmptyName()
        {
            string name = "";
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _customerBuilder.ApplyName(name).Build();
            }).WithMessage(Customer.InvalidNameErrorMessage);
        }

        [Fact]
        public void ShouldNotCreateCustomerWithFutureSince()
        {
            DateTime since = DateTime.Now.AddDays(5);
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _customerBuilder.ApplySince(since).Build();
            }).WithMessage(Customer.InvalidSinceErrorMessage);
        }



    }
}
