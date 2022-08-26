using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interface;
using SharedKernel.Settings;
using Pharmacy.Infrastructure.Factories;
using Pharmacy.Infrastructure.Persistence;
using SharedKernel.Services;
using SharedKernel.Extensions.ServiceCollectionExtensions;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetValue("SaasWebURL", "https://saas.danphe.com"))
                          .AllowAnyHeader()
                          .AllowAnyMethod(); ;
                      });
});

// Application Services
builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

// Infrastructure
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddDbContext<PharmacyDbContext>();
builder.Services.AddScoped<PharmacyDbContextFactory>();
builder.Services.AddScoped<PharmacyDbContext>(sp =>
    sp.GetRequiredService<PharmacyDbContextFactory>().CreateDbContext()
);

// Web Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();

// Register the Swagger generator and the Swagger UI middlewares
app.UseOpenApi();
app.UseSwaggerUi3();


app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

var scope = app.Services.CreateScope();

var client = new HttpClient();

HttpResponseMessage response = await client.GetAsync(builder.Configuration.GetValue("SaasWebURL", "https://saas.danphe.com") + "/api/tenant");
if (response.IsSuccessStatusCode)
{
    var tenants = await response.Content.ReadFromJsonAsync<List<Tenant>>();
    foreach (Tenant? tenant in tenants)
    {
        var tenantService = (ITenantService)new TenantService(tenant);
        // instantiate and migrate all db context for tenants
        var dbContext = new PharmacyDbContextFactory(tenantService).CreateDbContext();
        dbContext.Database.Migrate();
    }
}

scope.Dispose();


app.Run();


