namespace Core.Security.Jwt.Dtos;

public class TokenOption
{
    #region Properties

    public int TokenExpires { get; set; }
    public int RefresTokenExpires { get; set; }
    public string SecurityKey { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateLifetime { get; set; }

    #endregion Properties
}
