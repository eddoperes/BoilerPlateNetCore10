using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services.Super;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using System;

namespace BoilerPlateNetCore10.Application.Services
{
    public class EnterpriseService: CrudService<EnterpriseDTO, Enterprise>, IEnterpriseService
    {

        private readonly IEnterpriseRepository _enterpriseRepository;

        public EnterpriseService(IEnterpriseRepository enterpriseRepository) : base(enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository ?? throw new ArgumentNullException(nameof(IEnterpriseRepository));
        }

    }
}
