using Core.Security.Entities;
using Core.Security.Jwt.Abstractions;
using Core.Security.Jwt.Constants;
using Core.Security.Jwt.Dtos;
using Core.Security.Jwt.Encryption;
using Core.Security.Jwt.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Security.Jwt.Concretes;

public class JwtHelper : ITokenHelper
{
    #region Fields

    private readonly UserManager<AppUser> _userManager;
    private readonly Dtos.TokenOptions _tokenOption;

    #endregion Fields

    #region Constructors

    public JwtHelper(UserManager<AppUser> userManager, IOptions<Dtos.TokenOptions> tokenOption)
    {
        _userManager = userManager;
        _tokenOption = tokenOption.Value;
    }

    #endregion Constructors

    #region Methods

    public async Task<AccessToken> CreateTokenAsync(AppUser appUser)
    {
        await _userManager.ResetAccessFailedCountAsync(appUser);
        //await _userManager.UpdateAsync(appUser);

        DateTime tokenExpiry = DateTime.UtcNow.AddMinutes(_tokenOption.TokenExpires);
        RefreshToken refreshToken = GenerateRefreshToken(appUser);

        await _userManager.RemoveAuthenticationTokenAsync(appUser, AuthenticationTokenConstants.LoginProviderName, AuthenticationTokenConstants.RefreshToken);
        await _userManager.SetAuthenticationTokenAsync(appUser, AuthenticationTokenConstants.LoginProviderName, AuthenticationTokenConstants.RefreshToken, refreshToken.Token);

        //await _userManager.UpdateAsync(appUser);

        return new AccessToken
        {
            Token = GenerateToken(appUser, tokenExpiry),
            TokenExpiration = tokenExpiry,
            RefreshToken = refreshToken
        };
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenOption.SecurityKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

        if (validatedToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new UnauthorizedAccessException("Invalid token signature algorithm");

        int? expirationTime = jwtToken.Payload.Exp ?? throw new UnauthorizedAccessException("Invalid token.");
        DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime expirationDateTime = epoch.AddSeconds(expirationTime.Value);
        if (expirationDateTime < DateTime.UtcNow) throw new UnauthorizedAccessException("Token has expired");

        return claimsPrincipal;
    }

    private RefreshToken GenerateRefreshToken(AppUser appUser)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
        DateTime expiry = DateTime.UtcNow.AddHours(_tokenOption.RefresTokenExpires);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()) }),
            Expires = expiry,
            SigningCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey)
        };
        SecurityToken securitytToken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(securitytToken);

        RefreshToken refreshTokenDto = new()
        {
            Token = token,
            Expiry = expiry
        };
        return refreshTokenDto;
    }

    private string GenerateToken(AppUser appUser, DateTime tokenExpiry)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(GenerateTokenClaims(appUser).Result),
            Expires = tokenExpiry,
            SigningCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task<IEnumerable<Claim>> GenerateTokenClaims(AppUser appUser)
    {
        IEnumerable<string> appUserRoles = await _userManager.GetRolesAsync(appUser);

        List<Claim> claims = new();
        claims.AddNameIdentifier(appUser.Id.ToString());
        claims.AddRoles(appUserRoles);

        return claims;
    }

    #endregion Methods
}
