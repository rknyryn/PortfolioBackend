using Core.Abstractions.Mapper;
using Core.Application.Rules.Abstractions;
using Core.Application.Utilities.Wrappers;
using Core.Persistance.UnitOfWork.Abstractions;
using MediatR;
using Porfolio.Application.Features.Panel.Contacts.Rules;
using Porfolio.Application.Services.Repositories.ContactRepositories;
using Portfolio.Domain.Entities.Contacts;

namespace Porfolio.Application.Features.Panel.Contacts.Commands
{
    public class UpdateContactCommandRequest : IRequest<IMessageResult>
    {
        #region Properties

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #endregion Properties
    }

    public class UpdateContactCommandHandler
        : IRequestHandler<UpdateContactCommandRequest, IMessageResult>
    {
        #region Fields

        private readonly IReadContactRepository _readContactRepository;
        private readonly IWriteContactRepository _writeContactRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ContactRules _contactRules;

        #endregion Fields

        #region Constructors

        public UpdateContactCommandHandler(
            IReadContactRepository readContactRepository,
            IWriteContactRepository writeContactRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBusinessRuleFactory businessRuleFactory)
        {
            _readContactRepository = readContactRepository;
            _writeContactRepository = writeContactRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contactRules = businessRuleFactory.GetBusinessRule<ContactRules>();
        }

        #endregion Constructors

        #region Methods

        public async Task<IMessageResult> Handle(UpdateContactCommandRequest request, CancellationToken cancellationToken)
        {
            await _contactRules.CheckContactExistenceAsync(request.Id);

            Contact contact = await _readContactRepository.GetSingleAsync(
                predicate: p => p.Id == request.Id,
                cancellationToken: cancellationToken);

            contact = _mapper.Map<UpdateContactCommandRequest, Contact>(request, contact);

            _writeContactRepository.Update(contact);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(
                message: "İletişim bilgisi başarıyla güncellendi.");
        }

        #endregion Methods
    }
}
