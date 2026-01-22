using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services.Super;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using System;

namespace BoilerPlateNetCore10.Application.Services
{

    public class CustomerService : CrudService<CustomerDTO, Person>, ICustomerService
    {

        private readonly IPersonRepository _personRepository;

        public CustomerService(IPersonRepository personRepository) : base(personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(IPersonRepository));
        }

    }
}
