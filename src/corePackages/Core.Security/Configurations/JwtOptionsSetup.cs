using Core.Security.Jwt.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Porfolio.Application.Configurations;

public class JwtOptionsSetup : IConfigureOptions<TokenOptions>
{
    #region Fields

    private const string _sectionName = "TokenOptions";
    private readonly IConfiguration _configuration;

    #endregion Fields

    #region Constructors

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion Constructors

    #region Methods

    public void Configure(TokenOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }

    #endregion Methods
}
