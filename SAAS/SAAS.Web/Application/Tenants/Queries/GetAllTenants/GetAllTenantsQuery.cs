using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace SAAS.Web.Application.Tenants.Queries.GetAllTenants;

public class GetAllTenantsQuery : IRequest<List<TenantDTO>>
{
}

public class GetAllTenantsQueryHandler : IRequestHandler<GetAllTenantsQuery, List<TenantDTO>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTenantsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TenantDTO>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tenant.ProjectTo<TenantDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
