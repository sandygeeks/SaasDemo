using SharedKernel.Settings;

namespace SharedKernel.Interface;

public interface ITenantService
{
    string GetConnectionString();
    string GetDatabaseProvider();
    Tenant GetTenant();
}