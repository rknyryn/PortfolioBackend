using Core.Application.Rules.Factories;
using Core.Application.Utilities.Wrappers;
using Core.Security.Entities;
using Core.Security.Jwt.Abstractions;
using Core.Security.Jwt.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Porfolio.Application.Features.Panel.Authentications.Rules;
using System.Security.Claims;

namespace Porfolio.Application.Features.Panel.Authentications.Commands;

public class RefreshTokenCommandRequest : IRequest<IDataResult<AccessToken>>
{
    #region Properties

    public string RefreshToken { get; set; }

    #endregion Properties
}

public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommandRequest, IDataResult<AccessToken>>
{
    #region Fields

    private readonly ITokenHelper _tokenHelper;
    private readonly UserManager<AppUser> _userManager;
    private readonly AuthenticationRules _authenticationRules;

    #endregion Fields

    #region Constructors

    public RefreshTokenCommandHandler(
        ITokenHelper tokenHelper,
        UserManager<AppUser> userManager,
        IBusinessRuleFactory businessRuleFactory)
    {
        _tokenHelper = tokenHelper;
        _userManager = userManager;
        _authenticationRules = (AuthenticationRules)businessRuleFactory.GetBusinessRule(typeof(AuthenticationRules));
    }

    #endregion Constructors

    #region Methods

    public async Task<IDataResult<AccessToken>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal claims = _tokenHelper.ValidateToken(request.RefreshToken);
        string nameIdentifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser? appUser = await _userManager.FindByIdAsync(nameIdentifier);

        _authenticationRules.CheckIfAppUserExists(appUser);
        await _authenticationRules.CheckRefreshTokenExistsAsync(appUser, request.RefreshToken);

        AccessToken accessToken = await _tokenHelper.CreateTokenAsync(appUser);

        return new SuccessDataResult<AccessToken>(accessToken);
    }

    #endregion Methods
}
