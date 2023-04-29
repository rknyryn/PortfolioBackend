using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities.Contacts;

namespace Portfolio.Persistance.Mapping.Contacts;

public class ContactConfigurations : IEntityTypeConfiguration<Contact>
{
    #region Methods

    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable($"{nameof(Contact)}s");
    }

    #endregion Methods
}
