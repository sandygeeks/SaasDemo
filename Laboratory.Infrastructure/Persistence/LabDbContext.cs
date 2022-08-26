using Laboratory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Contracts;
using SharedKernel.Entities;
using SharedKernel.Interface;

namespace Laboratory.Infrastructure.Persistence;
public class LabDbContext : DbContext
{
    public int TenantId { get; set; }
    private readonly ITenantService _tenantService;
    public LabDbContext(DbContextOptions options, ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
        TenantId = _tenantService.GetTenant().Id;
    }

    public LabDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<LabTest> LabTests { get; set; }
    public DbSet<LabComponent> LabComponents { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Lab");
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<LabTest>().HasQueryFilter(a => a.TenantId == TenantId);
        modelBuilder.Entity<LabComponent>().HasQueryFilter(a => a.TenantId == TenantId);
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
