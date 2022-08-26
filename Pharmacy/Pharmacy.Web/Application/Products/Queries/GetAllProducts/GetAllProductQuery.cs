using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Entities;
using Pharmacy.Infrastructure.Persistence;
using SAAS.Pharmacy.Application.Common.Mappings;

namespace Pharmacy.Web.Application.Products.Queries.GetAllProducts;

public class GetAllProductQuery : IRequest<List<ProductDTO>>
{
}

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductDTO>>
{
    private readonly PharmacyDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(PharmacyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<ProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        return _context.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}


public class ProductDTO : IMapFrom<Product>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Rate { get; private set; }
}