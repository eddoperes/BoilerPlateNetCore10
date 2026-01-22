using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces.Super;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
    }
}
