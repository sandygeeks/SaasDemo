using SharedKernel.Entities;

namespace SAAS.Core.Entities;
public class Customer : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public string PaymentDetails { get; set; }
    public IList<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
