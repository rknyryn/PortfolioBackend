using Core.Persistance.Repositories.Concretes;
using Porfolio.Application.Services.Repositories.ContactRepositories;
using Portfolio.Domain.Entities.Contacts;
using Portfolio.Persistance.Contexts;

namespace Portfolio.Persistance.Repositories.ContactRepositories;

public class ReadContactRepository : EfReadRepository<Contact, AppDbContext>, IReadContactRepository
{
    #region Constructors

    public ReadContactRepository(AppDbContext context) : base(context)
    {
    }

    #endregion Constructors
}
