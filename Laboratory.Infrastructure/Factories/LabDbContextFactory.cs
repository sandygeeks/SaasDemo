using Laboratory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interface;
using SharedKernel.Settings;

namespace Laboratory.Infrastructure.Factories;
public class LabDbContextFactory
{
    private readonly ITenantService _tenantService;
    public Tenant _currentTenant;

    public LabDbContextFactory(ITenantService tenantService)
    {
        _tenantService = tenantService;
        _currentTenant = _tenantService.GetTenant();
    }
    public LabDbContext CreateDbContext()
    {
        var dbProvider = _currentTenant.DbProvider;
        var connectionString = _currentTenant.ConnectionString;

        var optionsBuilder = new DbContextOptionsBuilder<LabDbContext>();
        if (dbProvider == "SqlServer")
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Laboratory.Infrastructure.SqlServerMigration"));

        else if (dbProvider == "PostGreSql")
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("Laboratory.Infrastructure.PostGresMigration"));

        return new LabDbContext(optionsBuilder.Options, _tenantService);
    }
}
