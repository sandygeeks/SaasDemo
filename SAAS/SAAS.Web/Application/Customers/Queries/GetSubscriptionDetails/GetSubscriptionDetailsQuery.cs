using AutoMapper;
using AutoMapper.QueryableExtensions;
using SAAS.Core.Entities;
using SAAS.Web.Application.Common.Mappings;

namespace SAAS.Web.Application.Customers.Queries.GetSubscriptionDetail;

public class GetSubscriptionDetailsQuery : IRequest<List<SubscriptionDTO>>
{
    public int CustomerId { get; set; }
}

public class GetSubscriptionDetailsQueryHandler : IRequestHandler<GetSubscriptionDetailsQuery, List<SubscriptionDTO>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSubscriptionDetailsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SubscriptionDTO>> Handle(GetSubscriptionDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Subscriptions
             .ProjectTo<SubscriptionDTO>(_mapper.ConfigurationProvider)
             .Where(a => a.CustomerId == request.CustomerId)
             .ToListAsync(cancellationToken);


    }
}
public class SubscriptionDTO : IMapFrom<Subscription>
{
    public int CustomerId { get; set; }
    public string ModuleName { get; set; }
    public DateTime LicenseStartedOn { get; set; }
    public DateTime LicenseExpiredOn { get; set; }
    public bool Active { get; set; }
    public TenantDTO Tenant { get; set; }
}

public class TenantDTO : IMapFrom<Tenant>
{
    public int Id { get; set; }
    public string TenantUrl { get; set; }
    public string TenantName { get; set; }
    public string TenantAddress { get; set; }
    public string DbType { get; set; }
    public string DbProvider { get; set; }
    public string ConnectionString { get; set; }
}
