using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure.Persistence;

namespace Pharmacy.Web.Application.Commands.InitialiseDatabase;

public class InitialiseDatabaseCommand : IRequest<int>
{
}

public class InitialiseDatabaseCommandHandler : IRequestHandler<InitialiseDatabaseCommand, int>
{
    private readonly PharmacyDbContext _context;

    public InitialiseDatabaseCommandHandler(PharmacyDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(InitialiseDatabaseCommand request, CancellationToken cancellationToken)
    {
        await _context.Database.MigrateAsync();
        return await _context.SaveChangesAsync();
    }
}
