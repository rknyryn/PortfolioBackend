using Core.Persistance.Repositories.Concretes;
using Porfolio.Application.Services.Repositories.ContactRepositories;
using Portfolio.Domain.Entities.Contacts;
using Portfolio.Persistance.Contexts;

namespace Portfolio.Persistance.Repositories.ContactRepositories;

public class WriteContactRepository : EfWriteRepository<Contact, AppDbContext>, IWriteContactRepository
{
    #region Constructors

    public WriteContactRepository(AppDbContext context) : base(context)
    {
    }

    #endregion Constructors
}
