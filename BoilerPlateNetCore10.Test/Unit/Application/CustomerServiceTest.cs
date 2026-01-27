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
    public class CustomerServiceTest
    {


        private CustomerDTO _customerDTO;

        public CustomerServiceTest()
        {

            var faker = new Faker("pt_BR");

            _customerDTO = new()
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
        public async Task CreateCustomer_ValidDTO_ObjectAddedWithSuccess()
        {

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            ICustomerService customerService = new CustomerService(customerRepositoryMock.Object);
            await customerService.AddAsync(_customerDTO);

            customerRepositoryMock.Verify(a => a.CreateAsync(It.Is<Customer>(a =>
                                                                a.Id == _customerDTO.Id &&
                                                                a.Name == _customerDTO.Name &&
                                                                a.CPF.Number == _customerDTO.CPF.Number &&
                                                                a.Email.Address == _customerDTO.Email.Address &&
                                                                a.Phone.Number == _customerDTO.Phone.Number &&
                                                                a.Address.Street == _customerDTO.Address.Street &&
                                                                a.Address.Number == _customerDTO.Address.Number &&
                                                                a.Address.Complement == _customerDTO.Address.Complement &&
                                                                a.Address.Neighborhood == _customerDTO.Address.Neighborhood &&
                                                                a.Address.ZipCode == _customerDTO.Address.ZipCode &&
                                                                a.Address.City == _customerDTO.Address.City &&
                                                                a.Address.State == _customerDTO.Address.State
                                                              )));

        }

        [Fact]
        public async Task UpdateCustomer_ValidDTO_ObjectUpdatedWithSuccess()
        {

            /*
            Task<Customer?> GetByIdAsyncFake = Task<Customer?>.Factory.StartNew(() =>
            {
                var faker = new Faker("pt_BR");
                var customer = new Customer(id: _customerDTO.Id,
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
                return customer;
            });
            */

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            //customerRepositoryMock.Setup(c => c.GetByIdAsync(_customerDTO.Id)).Returns(GetByIdAsyncFake);
            ICustomerService customerService = new CustomerService(customerRepositoryMock.Object);
            await customerService.UpdateAsync(_customerDTO);

            customerRepositoryMock.Verify(a => a.UpdateAsync(It.Is<Customer>(a =>
                                                                a.Id == _customerDTO.Id &&
                                                                a.Name == _customerDTO.Name &&
                                                                a.CPF.Number == _customerDTO.CPF.Number &&
                                                                a.Email.Address == _customerDTO.Email.Address &&
                                                                a.Phone.Number == _customerDTO.Phone.Number &&
                                                                a.Address.Street == _customerDTO.Address.Street &&
                                                                a.Address.Number == _customerDTO.Address.Number &&
                                                                a.Address.Complement == _customerDTO.Address.Complement &&
                                                                a.Address.Neighborhood == _customerDTO.Address.Neighborhood &&
                                                                a.Address.ZipCode == _customerDTO.Address.ZipCode &&
                                                                a.Address.City == _customerDTO.Address.City &&
                                                                a.Address.State == _customerDTO.Address.State
                                                              )));

        }


        [Fact]
        public async Task RemoveCustomer_ValidDTO_ObjectRemovedWithSuccess()
        {

            /*
            Task<Customer?> GetByIdAsyncFake = Task<Customer?>.Factory.StartNew(() =>
            {
                var faker = new Faker("pt_BR");
                var customer = new Customer(id: _customerDTO.Id,
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
                return customer;
            });
            */

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            //customerRepositoryMock.Setup(c => c.GetByIdAsync(_customerDTO.Id)).Returns(GetByIdAsyncFake);
            ICustomerService customerService = new CustomerService(customerRepositoryMock.Object);
            await customerService.RemoveAsync(_customerDTO.Id);

            customerRepositoryMock.Verify(c => c.RemoveAsync(It.Is<long>(c => c == _customerDTO.Id)));

        }

    }

}
