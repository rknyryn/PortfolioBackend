using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.WebAPI.Areas.Panel;

[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "panel")]
[ApiController]
public class CustomControllerBase : ControllerBase
{
    #region Fields

    private IMediator _mediator;

    #endregion Fields

    #region Properties

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    #endregion Properties
}
