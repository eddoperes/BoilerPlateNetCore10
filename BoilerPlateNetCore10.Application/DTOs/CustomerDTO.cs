using BoilerPlateNetCore10.Application.DTOs.ValueObjects;
using System;

namespace BoilerPlateNetCore10.Application.DTOs
{
    public class CustomerDTO
    {

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public CPFDTO CPF { get;  set; } = new CPFDTO();

        public DateTime Since { get; set; }

        public long SupplierId { get; set; }

    }
}
