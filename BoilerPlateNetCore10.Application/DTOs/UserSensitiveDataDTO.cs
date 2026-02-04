using BoilerPlateNetCore10.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Application.DTOs
{
    public class UserSensitiveDataDTO
    {

        public long UserId { get; set; }

        public string Password { get; set; } = string.Empty;        

    }
}
