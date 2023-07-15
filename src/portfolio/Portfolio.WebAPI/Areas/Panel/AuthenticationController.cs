using Core.Application.Utilities.Wrappers;
using Core.Security.Jwt.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Porfolio.Application.Features.Panel.Authentications.Commands;

namespace Portfolio.WebAPI.Areas.Panel
{
    public class AuthenticationController : CustomControllerBase
    {
        #region Methods

        [HttpPost("Login")]
        public async Task<IDataResult<AccessToken>> Login(LoginCommandRequest request)
            => await Mediator.Send(request);

        [HttpPost("RefreshToken")]
        public async Task<IDataResult<AccessToken>> RefreshToken(RefreshTokenCommandRequest request)
            => await Mediator.Send(request);

        [HttpGet]
        [Authorize]
        public string Get()
        {
            return "Get";
        }

        #endregion Methods
    }
}
