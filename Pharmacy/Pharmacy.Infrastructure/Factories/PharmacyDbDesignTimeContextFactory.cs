using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pharmacy.Infrastructure.Persistence;

namespace Pharmacy.Infrastructure.Factories;
public class PharmacyDbDesignTimeContextFactory : IDesignTimeDbContextFactory<PharmacyDbContext>
{
    public PharmacyDbContext CreateDbContext(string[] args)
    {
        var dbProvider = args[0];
        var connectionString = args[1];

        var optionsBuilder = new DbContextOptionsBuilder<PharmacyDbContext>();
        if (dbProvider == "SqlServer")
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Pharmacy.Infrastructure.SqlServerMigration"));

        else if (dbProvider == "PostGreSql")
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("Pharmacy.Infrastructure"));


        return new PharmacyDbContext(optionsBuilder.Options);
    }
}
