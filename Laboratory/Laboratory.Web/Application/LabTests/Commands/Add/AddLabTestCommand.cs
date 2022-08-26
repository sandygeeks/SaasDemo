using Laboratory.Core.Entities;
using Laboratory.Infrastructure.Persistence;
using MediatR;

namespace Laboratory.Web.Application.LabTests.Commands.Add;

public class AddLabTestCommand : IRequest<int>
{
    public string Name { get; set; }
    public double Price { get; set; }

    public List<AddLabComponentsDTO> LabComponents { get; set; } = new List<AddLabComponentsDTO>();
}
public class AddLabComponentsDTO
{
    public string Name { get; set; }
}

public class AddLabTestCommandHandler : IRequestHandler<AddLabTestCommand, int>
{
    private readonly LabDbContext _context;

    public AddLabTestCommandHandler(LabDbContext context)
    {
        _context = context;
    }

    public Task<int> Handle(AddLabTestCommand request, CancellationToken cancellationToken)
    {
        var labTestEntity = new LabTest(request.Name, request.Price);
        labTestEntity.LabComponents = request.LabComponents.Select(lc => new LabComponent(lc.Name)).ToList();

        _context.LabTests.Add(labTestEntity);
        return _context.SaveChangesAsync();
    }
}