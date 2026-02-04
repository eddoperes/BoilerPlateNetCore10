using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.Validation;
using System;

namespace BoilerPlateNetCore10.Domain.Entities
{
    public class User : Entity
    {

        public static readonly string InvalidIdErrorMessage = "Invalid id. Smaller than zero.";
        public static readonly string InvalidNameErrorMessage = "Invalid name. Must be informed.";
        public static readonly string InvalidPermissionLevelErrorMessage = "Invalid permission level. Must be informed.";
        public static readonly string InvalidLoginErrorMessage = "Invalid login. Must be informed.";

        public User() { }

        public User(string name, 
                    string permissionLevel, string login,
                    string? refreshToken,
                    DateTime? refreshTokenExpire)
        {
            //called by entity framework
            Validate(name,
                     permissionLevel, login,
                     refreshToken,
                     refreshTokenExpire);
            Name = name;
            PermissionLevel = permissionLevel;
            Login = login;
            RefreshToken = refreshToken;
            RefreshTokenExpire = refreshTokenExpire;
        }

        public User(long id, string name, 
                    string permissionLevel, string login,
                    string? refreshToken,
                    DateTime? refreshTokenExpire)
        {
            //called by mapper
            DomainExceptionValidation.When(id < 0, InvalidIdErrorMessage);
            this.Id = id;
            Validate(name, 
                     permissionLevel, login,
                     refreshToken,
                     refreshTokenExpire);
            Name = name;
            PermissionLevel = permissionLevel;
            Login = login;
            RefreshToken = refreshToken;
            RefreshTokenExpire = refreshTokenExpire;
        }

        private void Validate(string name,
                              string permissionLevel, string login,
                              string? refreshToken,
                              DateTime? refreshTokenExpire)
        {
            DomainExceptionValidation.When(name.Length == 0, InvalidNameErrorMessage);
            DomainExceptionValidation.When(permissionLevel.Length == 0, InvalidPermissionLevelErrorMessage);
            DomainExceptionValidation.When(login.Length == 0, InvalidLoginErrorMessage);            
        }

        public string Name { get; private set; } = string.Empty;

        public string PermissionLevel { get; private set; } = string.Empty;

        public string Login { get; private set; } = string.Empty;

        public virtual UserSensitiveData? SensitiveData { get; private set; }

        public string? RefreshToken { get; private set; }

        public DateTime? RefreshTokenExpire { get; private set; }

        public void UpdatePasswordWithEncryptedVersion(string password)
        {
            this.SensitiveData?.UpdatePasswordWithEncryptedVersion(password);
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            this.RefreshToken = refreshToken;
        }

        public void UpdateLoginInfo(string refreshToken, DateTime refreshTokenExpire)
        {
            this.RefreshToken = refreshToken;
            this.RefreshTokenExpire = refreshTokenExpire;
        }     

    }
}
