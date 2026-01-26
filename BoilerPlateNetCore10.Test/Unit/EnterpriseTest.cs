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
    public class EnterpriseTest
    {

        private readonly Faker _faker;
        private readonly ITestOutputHelper _output;        
        private readonly EnterpriseBuilder _enterpriseBuilder = EnterpriseBuilder.GetEnterpriseBuilder();

        public EnterpriseTest(ITestOutputHelper output)
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
        public void MustCreateEnterprise()
        {

            //Arrange
            string name = _faker.Company.CompanyName();
            CNPJ cnpj = new CNPJ(_faker.Company.Cnpj());
            Email email = new Email(_faker.Person.Email);
            Phone phone = new Phone(_faker.Person.Phone.Replace("(","").Replace(")","").Replace(" ","").Replace("-","").Replace("+55",""));
            Address address = new Address(_faker.Address.StreetName(),
                                          _faker.Random.Int(100, 1000),
                                          _faker.Address.SecondaryAddress(),
                                          _faker.Address.StreetAddress(),
                                          _faker.Address.ZipCode().Replace("-",""),
                                          _faker.Address.City(),
                                          _faker.Address.StateAbbr());

            var expectedEnterprise = new
            {
                Name = name,
                CNPJ = cnpj,
                Email = email,
                Phone = phone,
                Address = address
            };

            //Act 
            var enterprise = _enterpriseBuilder.ApplyName(expectedEnterprise.Name)
                                               .ApplyCNPJ(expectedEnterprise.CNPJ)
                                               .ApplyEmail(expectedEnterprise.Email)
                                               .ApplyPhone(expectedEnterprise.Phone)
                                               .ApplyAddress(expectedEnterprise.Address)
                                               .Build();

            //Assert
            expectedEnterprise.ToExpectedObject().ShouldMatch(enterprise);

        }

        [Fact]
        public void ShouldNotCreateEnterpriseWithNegativeId()
        {
            long id = -1;
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _enterpriseBuilder.ApplyId(id).Build();
            }).WithMessage(Enterprise.InvalidIdErrorMessage);           
        }

        [Fact]
        public void ShouldNotCreateEnterpriseWithEmptyName()
        {
            string name = "";
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _enterpriseBuilder.ApplyName(name).Build();
            }).WithMessage(Enterprise.InvalidNameErrorMessage);        
        }


    }
}
