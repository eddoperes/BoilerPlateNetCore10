using Bogus;
using Bogus.Extensions.Brazil;
using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.DTOs.ValueObjects;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using BoilerPlateNetCore10.Domain.ValueObjects;
using Moq;

namespace BoilerPlateNetCore10.Test.Unit.Application
{
    public class EnterpriseServiceTest
    {

        private EnterpriseDTO _enterpriseDTO;

        public EnterpriseServiceTest()
        {

            var faker = new Faker("pt_BR");

            _enterpriseDTO = new()
            {
                Id = faker.Random.Long(1, 9999),
                Name = faker.Company.CompanyName(),
                CNPJ = new CNPJDTO { 
                    Number = faker.Company.Cnpj()
                },
                Email = new EmailDTO {
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
        public async Task CreateEnterprise_ValidDTO_ObjectAddedWithSuccess()
        {

            var enterpriseRepositoryMock = new Mock<IEnterpriseRepository>();
            IEnterpriseService enterpriseService = new EnterpriseService(enterpriseRepositoryMock.Object);
            await enterpriseService.AddAsync(_enterpriseDTO);

            enterpriseRepositoryMock.Verify(a => a.CreateAsync(It.Is<Enterprise>(a =>
                                                                a.Id == _enterpriseDTO.Id &&
                                                                a.Name == _enterpriseDTO.Name &&
                                                                a.CNPJ.Number == _enterpriseDTO.CNPJ.Number &&
                                                                a.Email.Address == _enterpriseDTO.Email.Address &&                                                               
                                                                a.Phone.Number == _enterpriseDTO.Phone.Number &&
                                                                a.Address.Street == _enterpriseDTO.Address.Street &&
                                                                a.Address.Number == _enterpriseDTO.Address.Number &&
                                                                a.Address.Complement == _enterpriseDTO.Address.Complement &&
                                                                a.Address.Neighborhood == _enterpriseDTO.Address.Neighborhood &&
                                                                a.Address.ZipCode == _enterpriseDTO.Address.ZipCode &&
                                                                a.Address.City == _enterpriseDTO.Address.City &&
                                                                a.Address.State == _enterpriseDTO.Address.State
                                                              )));

        }

        [Fact]
        public async Task UpdateEnterprise_ValidDTO_ObjectUpdatedWithSuccess()
        {

            /*
            Task<Enterprise?> GetByIdAsyncFake = Task<Enterprise?>.Factory.StartNew(() =>
            {
                var faker = new Faker("pt_BR");
                var enterprise = new Enterprise(id: _enterpriseDTO.Id,
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
                return enterprise;
            });
            */

            var enterpriseRepositoryMock = new Mock<IEnterpriseRepository>();
            //enterpriseRepositoryMock.Setup(c => c.GetByIdAsync(_enterpriseDTO.Id)).Returns(GetByIdAsyncFake);
            IEnterpriseService enterpriseService = new EnterpriseService(enterpriseRepositoryMock.Object);
            await enterpriseService.UpdateAsync(_enterpriseDTO);

            enterpriseRepositoryMock.Verify(a => a.UpdateAsync(It.Is<Enterprise>(a =>
                                                                a.Id == _enterpriseDTO.Id &&
                                                                a.Name == _enterpriseDTO.Name &&
                                                                a.CNPJ.Number == _enterpriseDTO.CNPJ.Number &&
                                                                a.Email.Address == _enterpriseDTO.Email.Address &&
                                                                a.Phone.Number == _enterpriseDTO.Phone.Number &&
                                                                a.Address.Street == _enterpriseDTO.Address.Street &&
                                                                a.Address.Number == _enterpriseDTO.Address.Number &&
                                                                a.Address.Complement == _enterpriseDTO.Address.Complement &&
                                                                a.Address.Neighborhood == _enterpriseDTO.Address.Neighborhood &&
                                                                a.Address.ZipCode == _enterpriseDTO.Address.ZipCode &&
                                                                a.Address.City == _enterpriseDTO.Address.City &&
                                                                a.Address.State == _enterpriseDTO.Address.State
                                                              )));

        }


        [Fact]
        public async Task RemoveEnterprise_ValidDTO_ObjectRemovedWithSuccess()
        {

            /*
            Task<Enterprise?> GetByIdAsyncFake = Task<Enterprise?>.Factory.StartNew(() =>
            {
                var faker = new Faker("pt_BR");
                var enterprise = new Enterprise(id: _enterpriseDTO.Id,
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
                return enterprise;
            });
            */

            var enterpriseRepositoryMock = new Mock<IEnterpriseRepository>();
            //enterpriseRepositoryMock.Setup(c => c.GetByIdAsync(_enterpriseDTO.Id)).Returns(GetByIdAsyncFake);
            IEnterpriseService enterpriseService = new EnterpriseService(enterpriseRepositoryMock.Object);
            await enterpriseService.RemoveAsync(_enterpriseDTO.Id);

            enterpriseRepositoryMock.Verify(c => c.RemoveAsync(It.Is<long>(c => c == _enterpriseDTO.Id)));

        }

    }
}
