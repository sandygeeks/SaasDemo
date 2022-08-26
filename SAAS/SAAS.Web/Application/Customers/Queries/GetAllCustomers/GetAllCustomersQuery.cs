using AutoMapper;
using AutoMapper.QueryableExtensions;
using SAAS.Core.Entities;
using SAAS.Web.Application.Common.Mappings;

namespace SAAS.Web.Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQuery : IRequest<List<CustomerDTO>>
{
}

public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDTO>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllCustomerQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<List<CustomerDTO>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return _context.Customers.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
    }
}

public class CustomerDTO : IMapFrom<Customer>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public string PaymentDetails { get; set; }
    public DateTime Created { get; set; }
}
