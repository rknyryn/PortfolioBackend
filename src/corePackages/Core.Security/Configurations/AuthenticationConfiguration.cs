using Core.Security.Jwt.Dtos;
using Core.Security.Jwt.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Security.Configurations;

public static class AuthenticationConfiguration
{
    #region Methods

    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOption>(configuration.GetSection("TokenOption"));
        TokenOption tokenOptions = configuration.GetSection("TokenOption").Get<TokenOption>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                ValidateIssuerSigningKey = tokenOptions.ValidateIssuerSigningKey,
                ValidateAudience = tokenOptions.ValidateAudience,
                ValidateIssuer = tokenOptions.ValidateIssuer,
                ValidateLifetime = tokenOptions.ValidateLifetime,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

    #endregion Methods
}
