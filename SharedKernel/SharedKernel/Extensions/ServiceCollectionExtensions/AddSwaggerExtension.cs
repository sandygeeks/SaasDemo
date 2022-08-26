using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SharedKernel.Extensions.ServiceCollectionExtensions;
public static class AddSwaggerExtension
{
    public static void AddSwagger(this IServiceCollection services)
    {
        string title = System.Reflection.Assembly.GetCallingAssembly().FullName;

        services.AddSwaggerDocument(config =>
        {
            config.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = title;
                document.Info.Description = $"A swagger application to check apis for {title}";
                document.Info.TermsOfService = "None";
                document.Info.Contact = new NSwag.OpenApiContact
                {
                    Name = "Sanjit Raj Shakya",
                    Email = "sandygivo@gmail.com",
                    Url = "https://twitter.com/sandygeeks"
                };
                document.Info.License = new NSwag.OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = "https://example.com/license"
                };
            };
        });

    }
}
