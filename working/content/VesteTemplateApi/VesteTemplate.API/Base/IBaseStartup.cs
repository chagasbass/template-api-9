using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace VesteTemplate.Api.Bases
{
    public interface IBaseStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider);
        void ConfigureServices(IServiceCollection services);
    }
}
