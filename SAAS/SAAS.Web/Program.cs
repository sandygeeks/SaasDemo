using SharedKernel.Extensions.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure Services
var dbProvider = builder.Configuration.GetValue("DbProvider", "PostGreSql");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => _ = dbProvider switch
    {
        "PostGreSql" => options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
        x => x.MigrationsAssembly("SAAS.Infrastructure.PostGreSql")
        ),

        "SqlServer" => options.UseSqlServer(
            builder.Configuration.GetConnectionString("Default"),
            x => x.MigrationsAssembly("SAAS.Infrastructure.SqlServer")
            ),

        _ => throw new Exception($"Unsupported provider: {dbProvider}")
    });
builder.Services.AddScoped(typeof(ApplicationDbContextInitialiser));

// Application Services
builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

// Web Services
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();
// Register the Swagger generator and the Swagger UI middlewares
app.UseOpenApi();
app.UseSwaggerUi3();
app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
await dbContextInitialiser.Initialise();


app.Run();
