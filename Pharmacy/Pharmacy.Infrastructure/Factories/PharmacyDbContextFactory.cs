using Microsoft.EntityFrameworkCore;
using SharedKernel.Interface;
using SharedKernel.Settings;
using Pharmacy.Infrastructure.Persistence;

namespace Pharmacy.Infrastructure.Factories;
public class PharmacyDbContextFactory : IDbContextFactory<PharmacyDbContext>
{
    private readonly ITenantService _tenantService;
    public Tenant _currentTenant;

    public PharmacyDbContextFactory(ITenantService tenantService)
    {
        _tenantService = tenantService;
        _currentTenant = _tenantService.GetTenant();
    }
    public PharmacyDbContext CreateDbContext()
    {
        var dbProvider = _currentTenant.DbProvider;
        var connectionString = _currentTenant.ConnectionString;

        var optionsBuilder = new DbContextOptionsBuilder<PharmacyDbContext>();
        if (dbProvider == "SqlServer")
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Pharmacy.Infrastructure.SqlServerMigration"));

        else if (dbProvider == "PostGreSql")
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("Pharmacy.Infrastructure.PostGresMigration"));

        return new PharmacyDbContext(optionsBuilder.Options, _tenantService);
    }
}
