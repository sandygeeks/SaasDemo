namespace SAAS.Web.Application.Tenants.Commands.UpdateUrl;

public class UpdateUrlCommand : IRequest<int>
{
    public int TenantId { get; set; }
    public string Url { get; set; }
}

public class UpdateUrlCommandHandler : IRequestHandler<UpdateUrlCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateUrlCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> Handle(UpdateUrlCommand request, CancellationToken cancellationToken)
    {
        var tenantEntity = _context.Tenant.Where(t => t.Id == request.TenantId).First();
        tenantEntity.TenantUrl = request.Url;
        return _context.SaveChangesAsync();
    }
}
