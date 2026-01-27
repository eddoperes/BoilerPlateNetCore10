using BoilerPlateNetCore10.Application.DTOs.ValueObjects;
using System;

namespace BoilerPlateNetCore10.Application.DTOs
{
    public class CustomerDTO
    {

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public CPFDTO CPF { get;  set; } = new CPFDTO();

        public EmailDTO Email { get; set; } = new EmailDTO();

        public PhoneDTO Phone { get; set; } = new PhoneDTO();

        public AddressDTO Address { get; set; } = new AddressDTO();

        public DateTime Since { get; set; }

        public long SupplierId { get; set; }

    }
}
