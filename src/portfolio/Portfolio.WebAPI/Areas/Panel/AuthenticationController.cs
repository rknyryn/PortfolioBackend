using Core.Application.Utilities.Wrappers;
using Core.Security.Jwt.Dtos;
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

        #endregion Methods
    }
}
