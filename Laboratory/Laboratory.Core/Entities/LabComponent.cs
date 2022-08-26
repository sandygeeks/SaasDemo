using SharedKernel.Contracts;
using SharedKernel.Entities;

namespace Laboratory.Core.Entities;
public class LabComponent : BaseAuditableEntity, IMustHaveTenant
{
    public LabComponent(string name)
    {
        Name = name;
    }

    protected LabComponent() { }

    public string Name { get; set; }
    public int TenantId { get; set; }
    public int LabTestId { get; set; }
    public LabTest LabTest { get; set; }

}
