using Laboratory.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Laboratory.Web.Application.Database.Commands.InitialiseDatabase;

public class InitialiseDatabaseCommand : IRequest<int>
{
}

public class InitialiseDatabaseCommandHandler : IRequestHandler<InitialiseDatabaseCommand, int>
{
    private readonly LabDbContext _context;

    public InitialiseDatabaseCommandHandler(LabDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(InitialiseDatabaseCommand request, CancellationToken cancellationToken)
    {
        await _context.Database.MigrateAsync();
        return await _context.SaveChangesAsync();
    }
}