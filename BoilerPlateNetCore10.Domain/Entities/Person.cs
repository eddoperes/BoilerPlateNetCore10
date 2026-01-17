using BoilerPlateNetCore10.Domain.Entities.Super;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public abstract class Person: Entity
    {

        public string Name { get; protected set; } = string.Empty;
        


    }


}
