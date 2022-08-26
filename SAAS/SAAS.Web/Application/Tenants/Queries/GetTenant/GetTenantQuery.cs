using AutoMapper;
using AutoMapper.QueryableExtensions;
using SAAS.Web.Application.Tenants.Queries.GetAllTenants;

namespace SAAS.Web.Application.Tenants.Queries.GetTenant;

public class GetTenantQuery : IRequest<TenantDTO>
{
    public int Id { get; set; }
}

public class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, TenantDTO>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTenantQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<TenantDTO> Handle(GetTenantQuery request, CancellationToken cancellationToken)
    {
        return _context.Tenant.ProjectTo<TenantDTO>(_mapper.ConfigurationProvider)
            .Where(t => t.Id == request.Id).FirstAsync(cancellationToken);
    }
}
