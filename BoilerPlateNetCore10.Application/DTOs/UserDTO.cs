using System;

namespace BoilerPlateNetCore10.Application.DTOs
{
    public class UserDTO
    {

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PermissionLevel { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public UserSensitiveDataDTO? SensitiveData { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpire { get; set; }

    }
}
