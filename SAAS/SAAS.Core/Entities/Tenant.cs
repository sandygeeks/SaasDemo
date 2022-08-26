using SharedKernel.Entities;

namespace SAAS.Core.Entities;
public class Tenant : BaseAuditableEntity
{
    public string TenantUrl { get; set; }
    public string TenantName { get; set; }
    public string TenantAddress { get; set; }
    public string DbType { get; set; } // Shared, Isolated
    public string DbProvider { get; set; }
    public string ConnectionString { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
}
