using BoilerPlateNetCore10.Application.DTOs.ValueObjects;
using System;

namespace BoilerPlateNetCore10.Application.DTOs
{
    public class EmployeeDTO
    {

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public CPFDTO CPF { get; set; } = new CPFDTO();

        public DateTime Admission { get; set; }

        public DateTime? Resignation { get; set; }

        public long EnterpriseId { get; set; }

    }
}
