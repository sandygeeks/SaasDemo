
namespace SharedKernel.Settings;
public class TenantSetting
{
    public List<Tenant> Tenants { get; set; }
}
public class Tenant
{
    public int Id { get; set; }
    public string TenantUrl { get; set; }
    public string TenantName { get; set; }
    public string TenantAddress { get; set; }
    public string DbType { get; set; } // Shared, Isolated
    public string DbProvider { get; set; }
    public string ConnectionString { get; set; }
}
