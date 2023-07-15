using Core.Abstractions.Mapper;
using Core.Application.Dtos;
using Core.Application.Utilities.Wrappers;
using Core.Persistance.Paging.Abstractions;
using Core.Persistance.Paging.ViewModels;
using MediatR;
using Porfolio.Application.Features.Panel.Contacts.ViewModels;
using Porfolio.Application.Services.Repositories.ContactRepositories;
using Portfolio.Domain.Entities.Contacts;

namespace Porfolio.Application.Features.Panel.Contacts.Queries
{
    public class GetPaginatedContactListQueryRequest : IRequest<IDataResult<BasePageableModel<ContactViewModel>>>
    {
        #region Properties

        public PaginationRequestDto Pagination { get; set; }

        #endregion Properties
    }

    public class GetPaginatedContactListQueryHandler
        : IRequestHandler<GetPaginatedContactListQueryRequest, IDataResult<BasePageableModel<ContactViewModel>>>
    {
        #region Fields

        private readonly IReadContactRepository _readContactRepository;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructors

        public GetPaginatedContactListQueryHandler(
            IReadContactRepository readContactRepository,
            IMapper mapper)
        {
            _readContactRepository = readContactRepository;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<IDataResult<BasePageableModel<ContactViewModel>>> Handle(GetPaginatedContactListQueryRequest request, CancellationToken cancellationToken)
        {
            IPaginate<Contact> contacts = await _readContactRepository.GetPaginatedListAsync(
                index: request.Pagination.PageIndex,
                size: request.Pagination.PageSize);

            BasePageableModel<ContactViewModel> model = _mapper.Map<IPaginate<Contact>, BasePageableModel<ContactViewModel>>(contacts);

            return new SuccessDataResult<BasePageableModel<ContactViewModel>>(model);
        }

        #endregion Methods
    }
}
