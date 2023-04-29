using Core.Application.Rules.Abstractions;
using Core.CrossCuttingConcern.Exceptions.Exceptions;
using Porfolio.Application.Services.Repositories.ContactRepositories;

namespace Porfolio.Application.Features.Panel.Contacts.Rules;

public class ContactRules : IBusinessRule
{
    #region Fields

    private readonly IReadContactRepository _readContactRepository;

    #endregion Fields

    #region Constructors

    public ContactRules(IReadContactRepository readContactRepository)
    {
        _readContactRepository = readContactRepository;
    }

    #endregion Constructors

    #region Methods

    public async Task CheckContactExistenceAsync(Guid contactId)
    {
        if(!await _readContactRepository.IsExistAsync(p => p.Id == contactId))
        {
            throw new BusinessException("İletişim bilgisi mevcut değil.");
        }
    }

    #endregion Methods
}
