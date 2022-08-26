using SharedKernel.Contracts;
using SharedKernel.Entities;

namespace Laboratory.Core.Entities;
public class LabTest : BaseAuditableEntity, IMustHaveTenant
{
    public LabTest(string name, double price)
    {
        Name = name;
        Price = price;
    }
    protected LabTest()
    {
    }
    public string Name { get; set; }
    public double Price { get; set; }
    public int TenantId { get; set; }
    public List<LabComponent> LabComponents { get; set; } = new List<LabComponent>();
}
