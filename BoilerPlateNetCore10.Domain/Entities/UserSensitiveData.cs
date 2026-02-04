using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Validation;


public class UserSensitiveData
{

    public static readonly string InvalidIdErrorMessage = "Invalid id. Smaller than zero.";
    public static readonly string InvalidPasswordErrorMessage = "Invalid password. Must be informed.";

    public UserSensitiveData()
    {     
    }

    public UserSensitiveData(long userId, string password)
    {
        //called by entity framework and mapper
        Validate(userId, password);
        UserId = userId;
        Password = password;
    }

    private void Validate(long userId, string password)
    {
        DomainExceptionValidation.When(userId < 0, InvalidIdErrorMessage);
        DomainExceptionValidation.When(password.Length == 0, InvalidPasswordErrorMessage);        
    }

    public long UserId { get; private set; }

    public virtual User? User { get; private set; }

    public string Password { get; private set; } = string.Empty;

    public void UpdatePasswordWithEncryptedVersion(string password)
    {
        this.Password = password;
    }

}

