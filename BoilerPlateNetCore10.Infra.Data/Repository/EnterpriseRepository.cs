using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using BoilerPlateNetCore10.Infra.Data.Context;
using BoilerPlateNetCore10.Infra.Data.Repository.Super;

namespace BoilerPlateNetCore10.Infra.Data.Repository
{
    public class EnterpriseRepository : GenericRepository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(ApplicationDbContext context) : base(context)
        {
        }



    }
}
