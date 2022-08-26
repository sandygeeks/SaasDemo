using SharedKernel.Entities;

namespace SAAS.Core.Entities;
public class Subscription : BaseAuditableEntity
{
    public string ModuleName { get; set; }
    public DateTime LicenseStartedOn { get; set; }
    public DateTime LicenseExpiredOn { get; set; }
    public bool Active { get; set; }
    public Tenant Tenant { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
