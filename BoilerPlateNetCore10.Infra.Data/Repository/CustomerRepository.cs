using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using BoilerPlateNetCore10.Infra.Data.Context;
using BoilerPlateNetCore10.Infra.Data.Repository.Super;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Infra.Data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository       
    {

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
