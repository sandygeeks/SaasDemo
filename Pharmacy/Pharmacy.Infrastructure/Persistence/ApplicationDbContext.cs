using Microsoft.EntityFrameworkCore;
using SharedKernel.Contracts;
using Pharmacy.Core.Entities;
using SharedKernel.Interface;
using SharedKernel.Entities;

namespace Pharmacy.Infrastructure.Persistence;
public class PharmacyDbContext : DbContext
{
    public int TenantId { get; set; }
    private readonly ITenantService _tenantService;
    public PharmacyDbContext(DbContextOptions options, ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
        TenantId = _tenantService.GetTenant().Id;
    }

    public PharmacyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Pharmacy");
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasQueryFilter(a => a.TenantId == TenantId);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = 1;
                entry.Entity.Created = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = 1;
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.TenantId = TenantId;
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

}
