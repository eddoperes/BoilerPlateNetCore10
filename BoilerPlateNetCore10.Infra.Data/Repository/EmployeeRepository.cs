using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using BoilerPlateNetCore10.Infra.Data.Context;
using BoilerPlateNetCore10.Infra.Data.Repository.Super;

namespace BoilerPlateNetCore10.Infra.Data.Repository
{
    
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
    
}

