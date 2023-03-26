namespace Core.Security.Jwt.Dtos;

public class RefreshToken
{
    #region Properties

    public DateTime Expiry { get; set; }
    public string Token { get; set; }

    #endregion Properties
}
