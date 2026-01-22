using BoilerPlateNetCore10.Application.DTOs.ValueObjects;
using BoilerPlateNetCore10.Domain.ValueObjects;

namespace BoilerPlateNetCore10.Application.DTOs
{
    public class EnterpriseDTO
    {

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public CNPJDTO CNPJ { get; set; } = new CNPJDTO();

        public EmailDTO Email { get; set; } = new EmailDTO();

        public PhoneDTO Phone { get; set; } = new PhoneDTO();

        public AddressDTO Address { get; set; } = new AddressDTO();

    }
}
