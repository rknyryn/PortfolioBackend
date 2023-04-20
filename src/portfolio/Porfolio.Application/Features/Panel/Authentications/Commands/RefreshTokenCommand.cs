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

    #endregion Fields

    #region Constructors

    public RefreshTokenCommandHandler(
        ITokenHelper tokenHelper,
        UserManager<AppUser> userManager)
    {
        _tokenHelper = tokenHelper;
        _userManager = userManager;
    }

    #endregion Constructors

    #region Methods

    public async Task<IDataResult<AccessToken>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        AuthenticationRules authenticationRules = new(_userManager);

        ClaimsPrincipal claims = _tokenHelper.ValidateToken(request.RefreshToken);
        string nameIdentifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser? appUser = await _userManager.FindByIdAsync(nameIdentifier);

        authenticationRules.CheckIfAppUserExists(appUser);
        await authenticationRules.IsRefreshTokenExistAsync(appUser, request.RefreshToken);

        AccessToken accessToken = await _tokenHelper.CreateTokenAsync(appUser);

        return new SuccessDataResult<AccessToken>(accessToken);
    }

    #endregion Methods
}
