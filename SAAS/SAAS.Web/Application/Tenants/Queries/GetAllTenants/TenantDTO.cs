using SAAS.Core.Entities;
using SAAS.Web.Application.Common.Mappings;

namespace SAAS.Web.Application.Tenants.Queries.GetAllTenants;

public class TenantDTO : IMapFrom<Tenant>
{
    public int Id { get; set; }
    public string TenantUrl { get; set; }
    public string TenantName { get; set; }
    public string TenantAddress { get; set; }
    public string DbType { get; set; } // Shared, Isolated
    public string DbProvider { get; set; }
    public string ConnectionString { get; set; }
}
