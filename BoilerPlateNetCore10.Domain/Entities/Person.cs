using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public abstract class Person: Entity
    {

        public Person(string name, CPF cpf)
        {
            Name = name;
            CPF = cpf;
        }


        public string Name { get; protected set; } 
        
        public CPF CPF { get; protected set; }


    }


}
