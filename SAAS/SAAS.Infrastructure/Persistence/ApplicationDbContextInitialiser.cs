using Microsoft.EntityFrameworkCore;

namespace SAAS.Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Initialise()
    {
        _context.Database.Migrate();
        await _context.SaveChangesAsync();
    }
}
