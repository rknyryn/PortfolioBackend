using Core.Abstractions.Mapper;
using Core.Application.Utilities.Wrappers;
using Core.Persistance.UnitOfWork.Abstractions;
using MediatR;
using Porfolio.Application.Features.Panel.Contacts.ViewModels;
using Porfolio.Application.Services.Repositories.ContactRepositories;
using Portfolio.Domain.Entities.Contacts;

namespace Porfolio.Application.Features.Panel.Contacts.Commands;

public class CreateContactCommandRequest : IRequest<IDataResult<ContactViewModel>>
{
    #region Properties

    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    #endregion Properties
}

public class CreateContactCommandHandler
    : IRequestHandler<CreateContactCommandRequest, IDataResult<ContactViewModel>>
{
    #region Fields

    private readonly IWriteContactRepository _writeContactRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    #endregion Fields

    #region Constructors

    public CreateContactCommandHandler(
        IWriteContactRepository writeContactRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _writeContactRepository = writeContactRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    #endregion Constructors

    #region Methods

    public async Task<IDataResult<ContactViewModel>> Handle(CreateContactCommandRequest request, CancellationToken cancellationToken)
    {
        Contact contact = _mapper.Map<CreateContactCommandRequest, Contact>(request);

        _writeContactRepository.Add(contact);
        await _unitOfWork.SaveChangesAsync();
        ContactViewModel model = _mapper.Map<Contact, ContactViewModel>(contact);

        return new SuccessDataResult<ContactViewModel>(
        model,
        "İletişim bilgisi başarıyla oluşturuldu.");
    }

    #endregion Methods
}
