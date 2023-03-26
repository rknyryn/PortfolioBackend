using Microsoft.IdentityModel.Tokens;

namespace Core.Security.Jwt.Encryption;

public class SigningCredentialsHelper
{
    #region Methods

    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }

    #endregion Methods
}
