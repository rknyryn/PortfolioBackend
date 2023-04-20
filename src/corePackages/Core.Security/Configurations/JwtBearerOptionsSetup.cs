using Core.Security.Jwt.Dtos;
using Core.Security.Jwt.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Core.Security.Configurations;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    #region Fields

    private readonly TokenOptions _tokenOptions;

    #endregion Fields

    #region Constructors

    public JwtBearerOptionsSetup(IOptions<Jwt.Dtos.TokenOptions> tokenOption)
    {
        _tokenOptions = tokenOption.Value;
    }

    #endregion Constructors

    #region Methods

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey),
            ClockSkew = TimeSpan.Zero
        };
    }

    #endregion Methods
}
