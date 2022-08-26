using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedKernel.Interface;
using SharedKernel.Settings;
using System.Net.Http.Json;

namespace SharedKernel.Services;


public class TenantService : ITenantService
{
    private readonly IConfiguration _configuration;
    private HttpContext _httpContext;
    private Tenant _currentTenant;
    public TenantService(Tenant tenant)
    {
        SetTenant(tenant);
    }
    public TenantService(IHttpContextAccessor contextAccessor, IConfiguration configuration)
    {
        _configuration = configuration;

        _httpContext = contextAccessor.HttpContext;
        if (_httpContext != null)
        {
            if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
            {
                var client = new HttpClient();
                HttpResponseMessage response = Task.Run(
                    () => client.GetAsync(_configuration.GetRequiredSection("SaasWebURL").Value + "/api/tenant/" + tenantId)
                    ).Result;

                if (response.IsSuccessStatusCode)
                {
                    var currentTenant = response.Content.ReadFromJsonAsync<Tenant>().Result;
                    SetTenant(currentTenant);
                }
            }
            else
            {
                throw new Exception("Invalid Tenant!");
            }
        }

    }
    private void SetTenant(Tenant tenant)
    {
        _currentTenant = tenant;
        if (_currentTenant == null) throw new Exception("Invalid Tenant!");
    }

    public string GetConnectionString()
    {
        return _currentTenant?.ConnectionString;
    }
    public string GetDatabaseProvider()
    {
        return _currentTenant.DbProvider;
    }
    public Tenant GetTenant()
    {
        return _currentTenant;
    }
}
