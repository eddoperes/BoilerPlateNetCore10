using Bogus;
using Bogus.Extensions.Brazil;
using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.DTOs.ValueObjects;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using Moq;

namespace BoilerPlateNetCore10.Test.Unit.Application
{
    public class EmployeeServiceTest
    {

        private EmployeeDTO _employeeDTO;

        public EmployeeServiceTest()
        {

            var faker = new Faker("pt_BR");

            _employeeDTO = new()
            {
                Id = faker.Random.Long(1, 9999),
                Name = faker.Company.CompanyName(),
                CPF = new CPFDTO
                {
                    Number = faker.Person.Cpf()
                },
                Email = new EmailDTO
                {
                    Address = faker.Person.Email
                },
                Phone = new PhoneDTO
                {
                    Number = faker.Phone.PhoneNumber("219########")
                },
                Address = new AddressDTO
                {
                    Street = faker.Address.StreetName(),
                    Number = faker.Random.Int(100, 1000),
                    Complement = faker.Address.SecondaryAddress(),
                    Neighborhood = faker.Address.StreetAddress(),
                    ZipCode = faker.Address.ZipCode().Replace("-", ""),
                    City = faker.Address.City(),
                    State = faker.Address.StateAbbr()
                }
            };

        }

        // 1 - What test
        // 2 - Complement
        // 3 - Expected result

        [Fact]
        public async Task CreateEmployee_ValidDTO_ObjectAddedWithSuccess()
        {

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            IEmployeeService employeeService = new EmployeeService(employeeRepositoryMock.Object);
            await employeeService.AddAsync(_employeeDTO);

            employeeRepositoryMock.Verify(a => a.CreateAsync(It.Is<Employee>(a =>
                                                                a.Id == _employeeDTO.Id &&
                                                                a.Name == _employeeDTO.Name &&
                                                                a.CPF.Number == _employeeDTO.CPF.Number &&
                                                                a.Email.Address == _employeeDTO.Email.Address &&
                                                                a.Phone.Number == _employeeDTO.Phone.Number &&
                                                                a.Address.Street == _employeeDTO.Address.Street &&
                                                                a.Address.Number == _employeeDTO.Address.Number &&
                                                                a.Address.Complement == _employeeDTO.Address.Complement &&
                                                                a.Address.Neighborhood == _employeeDTO.Address.Neighborhood &&
                                                                a.Address.ZipCode == _employeeDTO.Address.ZipCode &&
                                                                a.Address.City == _employeeDTO.Address.City &&
                                                                a.Address.State == _employeeDTO.Address.State
                                                              )));

        }

        [Fact]
        public async Task UpdateEmployee_ValidDTO_ObjectUpdatedWithSuccess()
        {

            /*
            Task<Employee?> GetByIdAsyncFake = Task<Employee?>.Factory.StartNew(() =>
            {
                var faker = new Faker("pt_BR");
                var employee = new Employee(id: _employeeDTO.Id,
                                                name : faker.Company.CompanyName(),
                                                cnpj : new CNPJ(faker.Company.Cnpj()),
                                                email : new Email(faker.Person.Email),
                                                phone : new Phone(faker.Phone.PhoneNumber("##9########")),
                                                address : new Address(street: faker.Address.StreetName(),
                                                                      number: faker.Random.Int(100, 1000),
                                                                      complement: faker.Address.SecondaryAddress(),
                                                                      neighborhood: faker.Address.StreetAddress(),
                                                                      zipCode: faker.Address.ZipCode().Replace("-", ""),
                                                                      city: faker.Address.City(),
                                                                      state: faker.Address.StateAbbr())
                                      );
                return employee;
            });
            */

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            //employeeRepositoryMock.Setup(c => c.GetByIdAsync(_employeeDTO.Id)).Returns(GetByIdAsyncFake);
            IEmployeeService employeeService = new EmployeeService(employeeRepositoryMock.Object);
            await employeeService.UpdateAsync(_employeeDTO);

            employeeRepositoryMock.Verify(a => a.UpdateAsync(It.Is<Employee>(a =>
                                                                a.Id == _employeeDTO.Id &&
                                                                a.Name == _employeeDTO.Name &&
                                                                a.CPF.Number == _employeeDTO.CPF.Number &&
                                                                a.Email.Address == _employeeDTO.Email.Address &&
                                                                a.Phone.Number == _employeeDTO.Phone.Number &&
                                                                a.Address.Street == _employeeDTO.Address.Street &&
                                                                a.Address.Number == _employeeDTO.Address.Number &&
                                                                a.Address.Complement == _employeeDTO.Address.Complement &&
                                                                a.Address.Neighborhood == _employeeDTO.Address.Neighborhood &&
                                                                a.Address.ZipCode == _employeeDTO.Address.ZipCode &&
                                                                a.Address.City == _employeeDTO.Address.City &&
                                                                a.Address.State == _employeeDTO.Address.State
                                                              )));

        }


        [Fact]
        public async Task RemoveEmployee_ValidDTO_ObjectRemovedWithSuccess()
        {

            /*
            Task<Employee?> GetByIdAsyncFake = Task<Employee?>.Factory.StartNew(() =>
            {
                var faker = new Faker("pt_BR");
                var employee = new Employee(id: _employeeDTO.Id,
                                                name: faker.Company.CompanyName(),
                                                cnpj: new CNPJ(faker.Company.Cnpj()),
                                                email: new Email(faker.Person.Email),
                                                phone: new Phone(faker.Phone.PhoneNumber("##9########")),
                                                address: new Address(street: faker.Address.StreetName(),
                                                                      number: faker.Random.Int(100, 1000),
                                                                      complement: faker.Address.SecondaryAddress(),
                                                                      neighborhood: faker.Address.StreetAddress(),
                                                                      zipCode: faker.Address.ZipCode().Replace("-", ""),
                                                                      city: faker.Address.City(),
                                                                      state: faker.Address.StateAbbr())
                                      );
                return employee;
            });
            */

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            //employeeRepositoryMock.Setup(c => c.GetByIdAsync(_employeeDTO.Id)).Returns(GetByIdAsyncFake);
            IEmployeeService employeeService = new EmployeeService(employeeRepositoryMock.Object);
            await employeeService.RemoveAsync(_employeeDTO.Id);

            employeeRepositoryMock.Verify(c => c.RemoveAsync(It.Is<long>(c => c == _employeeDTO.Id)));

        }

    }
}
