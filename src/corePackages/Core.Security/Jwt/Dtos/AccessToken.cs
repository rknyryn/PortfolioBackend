namespace Core.Security.Jwt.Dtos;

public class AccessToken
{
    #region Properties

    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public RefreshToken RefreshToken { get; set; }

    #endregion Properties
}
