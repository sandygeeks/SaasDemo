using SharedKernel.Contracts;
using SharedKernel.Entities;

namespace Pharmacy.Core.Entities;
public class Product : BaseAuditableEntity, IMustHaveTenant
{
    public Product(string name, string description, int rate)
    {
        Name = name;
        Description = description;
        Rate = rate;
    }
    protected Product()
    {
    }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Rate { get; private set; }
    public int TenantId { get; set; }
}