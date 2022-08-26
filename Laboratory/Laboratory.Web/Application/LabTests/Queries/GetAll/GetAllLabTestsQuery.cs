using MediatR;
using SAAS.Laboratory.Application.Common.Mappings;
using Laboratory.Core.Entities;
using Laboratory.Infrastructure.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Laboratory.Web.Application.LabTests.Queries.GetAll;

public class GetAllLabTestsQuery : IRequest<List<LabTestDTO>>
{
}

public class GetAllLabTestsHandler : IRequestHandler<GetAllLabTestsQuery, List<LabTestDTO>>
{
    private readonly LabDbContext _context;
    private readonly IMapper _mapper;

    public GetAllLabTestsHandler(LabDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<LabTestDTO>> Handle(GetAllLabTestsQuery request, CancellationToken cancellationToken)
    {
        return _context.LabTests.ProjectTo<LabTestDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
}


public class LabTestDTO : IMapFrom<LabTest>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public List<LabComponentDTO> LabComponents { get; set; }

}

public class LabComponentDTO : IMapFrom<LabComponent>
{
    public string Name { get; set; }
}