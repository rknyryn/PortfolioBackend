using Core.Application.Rules.Factories;
using Core.Application.Utilities.Wrappers;
using Core.Persistance.UnitOfWork.Abstractions;
using MediatR;
using Porfolio.Application.Features.Panel.Contacts.Rules;
using Porfolio.Application.Services.Repositories.ContactRepositories;

namespace Porfolio.Application.Features.Panel.Contacts.Commands
{
    public class DeleteContactCommandRequest : IRequest<IMessageResult>
    {
        #region Properties

        public Guid Id { get; set; }

        #endregion Properties
    }

    public class DeleteContactCommandHandler
        : IRequestHandler<DeleteContactCommandRequest, IMessageResult>
    {
        #region Fields

        private readonly IWriteContactRepository _writeContactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ContactRules _contactRules;

        #endregion Fields

        #region Constructors

        public DeleteContactCommandHandler(
            IWriteContactRepository writeContactRepository,
            IUnitOfWork unitOfWork,
            IBusinessRuleFactory businessRuleFactory)
        {
            _writeContactRepository = writeContactRepository;
            _unitOfWork = unitOfWork;
            _contactRules = (ContactRules)businessRuleFactory.GetBusinessRule(typeof(ContactRules));
        }

        #endregion Constructors

        #region Methods

        public async Task<IMessageResult> Handle(DeleteContactCommandRequest request, CancellationToken cancellationToken)
        {
            await _contactRules.CheckContactExistenceAsync(request.Id);

            await _writeContactRepository.DeleteByIdAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(
                message: "İletişim bilgisi başarıyla silindi.");
        }

        #endregion Methods
    }
}
