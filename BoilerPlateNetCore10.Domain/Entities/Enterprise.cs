using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public class Enterprise: Entity
    {

        public CNPJ CNPJ { get; protected set; } = new CNPJ();

    }
}
