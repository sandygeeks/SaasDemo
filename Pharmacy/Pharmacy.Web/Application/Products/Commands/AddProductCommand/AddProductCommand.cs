using AutoMapper;
using MediatR;
using Pharmacy.Core.Entities;
using Pharmacy.Infrastructure.Persistence;

namespace Pharmacy.Web.Application.Products.Commands.AddProductCommand;

public class AddProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; }
}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
{
    private readonly PharmacyDbContext _context;

    public AddProductCommandHandler(PharmacyDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description, request.Rate);

        _context.Add(product);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
