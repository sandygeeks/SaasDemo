using System.Net.Http.Headers;
using System.Text;

namespace SAAS.Web.Application.Tenants.Commands.InitialiseTenant;

public class InitialiseTenantCommand : IRequest<int>
{
    public int TenantId { get; set; }
}

public class InitialiseTenantCommandHandler : IRequestHandler<InitialiseTenantCommand, int>
{
    private readonly ApplicationDbContext _context;
    private HttpClient _httpClient;
    public InitialiseTenantCommandHandler(IHttpClientFactory httpClientFactory, ApplicationDbContext context)
    {
        _httpClient = httpClientFactory.CreateClient();
        _context = context;
    }
    public async Task<int> Handle(InitialiseTenantCommand request, CancellationToken cancellationToken)
    {
        var selectedTenant = await _context.Tenant.FirstAsync(a => a.Id == request.TenantId);

        _httpClient.BaseAddress = new Uri(selectedTenant.TenantUrl);

        var content = new StringContent("{}", Encoding.UTF8, "application/json");
        content.Headers.Add("tenant", selectedTenant.Id.ToString());
        var response = await _httpClient.PostAsync("/api/Database/Initialise", content);

        if (response.IsSuccessStatusCode)
        {
            return await Task.FromResult(1);
        }
        return await Task.FromResult(0);
    }
}
