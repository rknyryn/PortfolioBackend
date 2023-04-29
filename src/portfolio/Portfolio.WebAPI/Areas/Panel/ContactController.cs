using Core.Application.Utilities.Wrappers;
using Core.Persistance.Paging;
using Microsoft.AspNetCore.Mvc;
using Porfolio.Application.Features.Panel.Contacts.Commands;
using Porfolio.Application.Features.Panel.Contacts.Queries;
using Porfolio.Application.Features.Panel.Contacts.ViewModels;

namespace Portfolio.WebAPI.Areas.Panel
{
    public class ContactController : CustomControllerBase
    {
        #region Methods

        [HttpPost]
        public async Task<IDataResult<ContactViewModel>> Create(CreateContactCommandRequest request)
            => await Mediator.Send(request);

        [HttpGet("Paginated")]
        public async Task<IDataResult<BasePageableModel<ContactViewModel>>> GetPaginatedList([FromQuery] GetPaginatedContactListQueryRequest request)
            => await Mediator.Send(request);

        [HttpPut("Active")]
        public async Task<IMessageResult> Active(ActiveContactCommandRequest request)
            => await Mediator.Send(request);

        #endregion Methods
    }
}
