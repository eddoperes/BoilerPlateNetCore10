using Bogus;
using Bogus.Extensions.Brazil;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Validation;
using BoilerPlateNetCore10.Domain.ValueObjects;
using BoilerPlateNetCore10.Test.Unit.Builder;
using BoilerPlateNetCore10.Test.Util;
using ExpectedObjects;
using Xunit.Abstractions;

namespace BoilerPlateNetCore10.Test.Unit.Domain
{
    public class EmployeeTest
    {

        private readonly Faker _faker;
        private readonly ITestOutputHelper _output;
        private readonly EmployeeBuilder _employeeBuilder = EmployeeBuilder.GetEmployeeBuilder();

        public EmployeeTest(ITestOutputHelper output)
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
        public void MustCreateEmployee()
        {

            //Arrange
            string name = _faker.Person.FullName;
            CPF cpf = new CPF(_faker.Person.Cpf());
            Email email = new Email(_faker.Person.Email);
            Phone phone = new Phone(_faker.Phone.PhoneNumber("119########"));
            Address address = new Address(_faker.Address.StreetName(),
                                          _faker.Random.Int(100, 1000),
                                          _faker.Address.SecondaryAddress(),
                                          _faker.Address.StreetAddress(),
                                          _faker.Address.ZipCode().Replace("-", ""),
                                          _faker.Address.City(),
                                          _faker.Address.StateAbbr());
            DateTime admission = _faker.Date.Past(500);
            DateTime resignation = admission.AddYears(2);
            long employerId = _faker.Random.Long(1, 1000);

            var expectedEmployee = new
            {
                Name = name,
                CPF = cpf,
                Email = email,
                Phone = phone,
                Address = address,
                Admission = admission,
                Resignation = resignation,
                EmployerId = employerId
            };

            //Act 
            var employee = _employeeBuilder.ApplyName(expectedEmployee.Name)
                                           .ApplyCPF(expectedEmployee.CPF)
                                           .ApplyEmail(expectedEmployee.Email)
                                           .ApplyPhone(expectedEmployee.Phone)
                                           .ApplyAddress(expectedEmployee.Address)
                                           .ApplyAdmission(expectedEmployee.Admission)
                                           .ApplyResignation(expectedEmployee.Resignation)
                                           .ApplyEmployerId(expectedEmployee.EmployerId)
                                           .Build();

            //Assert
            expectedEmployee.ToExpectedObject().ShouldMatch(employee);

        }

        [Fact]
        public void ShouldNotCreateEmployeeWithNegativeId()
        {
            long id = -1;
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _employeeBuilder.ApplyId(id).Build();
            }).WithMessage(Employee.InvalidIdErrorMessage);
        }

        [Fact]
        public void ShouldNotCreateEmployeeWithEmptyName()
        {
            string name = "";
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _employeeBuilder.ApplyName(name).Build();
            }).WithMessage(Employee.InvalidNameErrorMessage);
        }

        [Fact]
        public void ShouldNotCreateEmployeeWithResignationBeforeAdmission()
        {
            DateTime admission = DateTime.Now.AddDays(-100);
            DateTime resignation = DateTime.Now.AddDays(-200);
            Assert.Throws<DomainExceptionValidation>(() =>
            {
                _employeeBuilder.ApplyAdmission(admission)
                                .ApplyResignation(resignation).Build();
            }).WithMessage(Employee.InvalidResignationErrorMessage);
        }


    }
}
