using Core.Application.Rules.Abstractions;
using Core.Application.Utilities.Wrappers;
using Core.Persistance.UnitOfWork.Abstractions;
using MediatR;
using Porfolio.Application.Features.Panel.Contacts.Rules;
using Porfolio.Application.Services.Repositories.ContactRepositories;
using Portfolio.Domain.Entities.Contacts;

namespace Porfolio.Application.Features.Panel.Contacts.Commands;

public class ActiveContactCommandRequest : IRequest<IMessageResult>
{
    #region Properties

    public Guid ContactId { get; set; }
    public bool Acitve { get; set; }

    #endregion Properties
}

public class ActiveContactCommandHandler
    : IRequestHandler<ActiveContactCommandRequest, IMessageResult>
{
    #region Fields

    private readonly IReadContactRepository _readContactRepository;
    private readonly IWriteContactRepository _writeContactRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ContactRules _contactRules;

    #endregion Fields

    #region Constructors

    public ActiveContactCommandHandler(
        IReadContactRepository readContactRepository,
        IWriteContactRepository writeContactRepository,
        IUnitOfWork unitOfWork,
        IBusinessRuleFactory businessRuleFactory)
    {
        _readContactRepository = readContactRepository;
        _writeContactRepository = writeContactRepository;
        _unitOfWork = unitOfWork;
        _contactRules = businessRuleFactory.GetBusinessRule<ContactRules>();
    }

    #endregion Constructors

    #region Methods

    public async Task<IMessageResult> Handle(ActiveContactCommandRequest request, CancellationToken cancellationToken)
    {
        await _contactRules.CheckContactExistenceAsync(request.ContactId);

        Contact contact = await _readContactRepository.GetSingleAsync(p => p.Id == request.ContactId);
        contact.Active = request.Acitve;

        _writeContactRepository.Update(contact);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult("İletişim bilgisi başarıyla güncellendi.");
    }

    #endregion Methods
}
