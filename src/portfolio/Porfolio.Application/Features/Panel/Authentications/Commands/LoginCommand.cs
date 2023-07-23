using Core.Application.Rules.Abstractions;
using Core.Application.Utilities.Wrappers;
using Core.CrossCuttingConcern.Exceptions.Exceptions;
using Core.Security.Entities;
using Core.Security.Jwt.Abstractions;
using Core.Security.Jwt.Constants;
using Core.Security.Jwt.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Porfolio.Application.Features.Panel.Authentications.Rules;

namespace Porfolio.Application.Features.Panel.Authentications.Commands;

public class LoginCommandRequest : IRequest<IDataResult<AccessToken>>
{
    #region Properties

    public string Email { get; set; }
    public string Password { get; set; }

    #endregion Properties
}

public class LoginCommandHandler
    : IRequestHandler<LoginCommandRequest, IDataResult<AccessToken>>
{
    #region Fields

    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHelper _tokenHelper;
    private readonly AuthenticationRules _authenticationRules;

    #endregion Fields

    #region Constructors

    public LoginCommandHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenHelper tokenHelper,
        IBusinessRuleFactory businessRuleFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHelper = tokenHelper;
        _authenticationRules = businessRuleFactory.GetBusinessRule<AuthenticationRules>();
    }

    #endregion Constructors

    #region Methods

    public async Task<IDataResult<AccessToken>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(request.Email);
        _authenticationRules.CheckIfAppUserExists(appUser);

        await SignInAsync(appUser, request.Password);
        AccessToken accessToken = await _tokenHelper.CreateTokenAsync(appUser);

        return new SuccessDataResult<AccessToken>(accessToken);
    }

    private async Task SignInAsync(AppUser user, string password)
    {
        await _signInManager.SignOutAsync();
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!signInResult.Succeeded)
        {
            await LoginAccessFailedAsync(user);
        }
    }

    private async Task LoginAccessFailedAsync(AppUser appUser)
    {
        await _userManager.AccessFailedAsync(appUser);
        int fail = await _userManager.GetAccessFailedCountAsync(appUser);
        if (fail >= SecurityConstants.MAX_FAILED_COUNT)
        {
            await _userManager.SetLockoutEndDateAsync(appUser, new DateTimeOffset(DateTime.UtcNow.AddMinutes(SecurityConstants.LOCK_TIME)));
            throw new BusinessException("");
        }
        else
        {
            throw new BusinessException("E-posta adresi veya şifre hatalı.");
        }
    }

    #endregion Methods
}
