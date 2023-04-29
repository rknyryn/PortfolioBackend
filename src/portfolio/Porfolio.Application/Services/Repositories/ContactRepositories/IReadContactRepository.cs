using Core.Persistance.Repositories.Abstractions;
using Portfolio.Domain.Entities.Contacts;

namespace Porfolio.Application.Services.Repositories.ContactRepositories;

public interface IReadContactRepository : IReadRepository<Contact>
{
}
