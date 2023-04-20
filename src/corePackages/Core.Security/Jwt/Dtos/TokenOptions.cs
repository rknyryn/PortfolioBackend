namespace Core.Security.Jwt.Dtos;

public class TokenOptions
{
    #region Properties

    public int TokenExpires { get; set; }
    public int RefresTokenExpires { get; set; }
    public string SecurityKey { get; set; }

    #endregion Properties
}
