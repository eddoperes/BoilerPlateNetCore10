using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services.Super;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using System;

namespace BoilerPlateNetCore10.Application.Services
{

    public class CustomerService : CrudService<CustomerDTO, Customer>, ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(ICustomerRepository));
        }

    }
}
