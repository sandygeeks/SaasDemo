using Laboratory.Infrastructure.Factories;
using Laboratory.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Extensions.ServiceCollectionExtensions;
using SharedKernel.Interface;
using SharedKernel.Services;
using SharedKernel.Settings;

var builder = WebApplication.CreateBuilder(args);



// Application Services
builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

// Infrastructure
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddDbContext<LabDbContext>();
builder.Services.AddScoped<LabDbContextFactory>();
builder.Services.AddScoped<LabDbContext>(sp =>
    sp.GetRequiredService<LabDbContextFactory>().CreateDbContext()
);

// Web Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();


// Register the Swagger generator and the Swagger UI middlewares
app.UseOpenApi();
app.UseSwaggerUi3();

app.MapControllers();

var scope = app.Services.CreateScope();

var client = new HttpClient();

HttpResponseMessage response = await client.GetAsync(builder.Configuration.GetValue("SaasWebURL", "https://saas.danphe.com") + "/api/tenant");
if (response.IsSuccessStatusCode)
{
    var tenants = await response.Content.ReadFromJsonAsync<List<Tenant>>();
    ArgumentNullException.ThrowIfNull(tenants);

    foreach (Tenant tenant in tenants)
    {
        var tenantService = (ITenantService)new TenantService(tenant);
        // instantiate and migrate all db context for tenants
        var dbContext = new LabDbContextFactory(tenantService).CreateDbContext();
        dbContext.Database.Migrate();
    }
}

scope.Dispose();


app.Run();
