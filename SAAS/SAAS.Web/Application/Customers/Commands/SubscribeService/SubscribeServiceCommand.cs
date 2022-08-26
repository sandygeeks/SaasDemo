using MediatR;
using SAAS.Core.Entities;

namespace SAAS.Web.Application.Customers.Commands.SubscribeService;

public class SubscribeServiceCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public string ModuleName { get; set; }
    public int Years { get; set; }
    public string TenantName { get; set; }
    public string TenantAddress { get; set; }
    public string DbType { get; set; }
    public string DbProvider { get; set; }
    public string ConnectionString { get; set; }
}

public class SubscribeServiceCommandHandler : IRequestHandler<SubscribeServiceCommand, int>
{
    private readonly ApplicationDbContext _context;

    public SubscribeServiceCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(SubscribeServiceCommand request, CancellationToken cancellationToken)
    {
        var customer = _context.Customers.First(a => a.Id == request.CustomerId);

        customer.Subscriptions.Add(new Subscription()
        {
            ModuleName = request.ModuleName,
            LicenseStartedOn = DateTime.UtcNow,
            LicenseExpiredOn = DateTime.UtcNow.AddYears(request.Years),
            Active = true,
            Tenant = new Tenant()
            {
                TenantName = request.TenantName,
                TenantUrl = request.TenantName.ToLower().Replace(" ", "") + ".danphe.com",
                TenantAddress = request.TenantAddress,
                DbType = request.DbType,
                DbProvider = request.DbProvider,
                ConnectionString = request.ConnectionString
            }
        });

        return await _context.SaveChangesAsync(cancellationToken);
    }
}