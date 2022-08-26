using Laboratory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Laboratory.Infrastructure.Factories;
public class LabDbDesignTimeContextFactory : IDesignTimeDbContextFactory<LabDbContext>
{
    public LabDbContext CreateDbContext(string[] args)
    {
        var dbProvider = args[0];
        var connectionString = args[1];

        var optionsBuilder = new DbContextOptionsBuilder<LabDbContext>();
        if (dbProvider == "SqlServer")
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Laboratory.Infrastructure.SqlServerMigration"));

        else if (dbProvider == "PostGreSql")
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("Laboratory.Infrastructure.PostGresMigration"));


        return new LabDbContext(optionsBuilder.Options);
    }
}
