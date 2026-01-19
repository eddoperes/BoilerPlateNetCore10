using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public class Employee : Person
    {
        public Employee(string name, CPF cpf) : base(name, cpf)
        {
        }
    }
}
