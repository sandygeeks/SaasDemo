using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Entities;
using Pharmacy.Infrastructure.Persistence;
using SAAS.Pharmacy.Application.Common.Mappings;

namespace Pharmacy.Web.Application.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdDTO>
{
    public int Id { get; set; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDTO>
{
    private readonly PharmacyDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(PharmacyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<GetProductByIdDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return _context.Products.ProjectTo<GetProductByIdDTO>(_mapper.ConfigurationProvider).FirstAsync(p => p.Id == request.Id);
    }
}

public class GetProductByIdDTO : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Rate { get; private set; }

}
