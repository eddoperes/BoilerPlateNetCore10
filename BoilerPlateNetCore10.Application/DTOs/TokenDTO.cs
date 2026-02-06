namespace BoilerPlateNetCore10.Application.DTOs
{
    public class TokenDTO
    {

        public TokenDTO(long userId, string userName, bool autenticated, 
                        string created, string expiration, 
                        string accessToken, string refreshToken,
                        string permissionLevel)
        {
            UserId = userId;
            UserName = userName;
            Autenticated = autenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            PermissionLevel = permissionLevel;
        }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public bool Autenticated { get; set; }

        public string Created { get; set; }

        public string Expiration { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string PermissionLevel { get; set; }

    }
}
