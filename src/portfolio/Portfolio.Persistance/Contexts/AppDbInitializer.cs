using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Portfolio.Persistance.Contexts;

public static class AppDbInitializer
{
    #region Methods

    public static void DbInitialize(IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        AppDbContext? context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        if (context != null)
        {
            if (!context.GetService<IDatabaseCreator>().CanConnect()) context.Database.Migrate();
            else if(context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
        }
    }

    #endregion Methods
}
