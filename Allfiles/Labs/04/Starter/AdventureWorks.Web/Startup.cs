using AdventureWorks.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.Configure<Settings>(
                _configuration.GetSection(nameof(Settings))
            );

            ConfigureProductService(services);
        }

        public void ConfigureProductService(IServiceCollection services)
        {            
            services.AddScoped<IAdventureWorksProductContext, AdventureWorksCosmosContext>(provider =>
    new AdventureWorksCosmosContext(
        _configuration.GetConnectionString(nameof(AdventureWorksCosmosContext))
    )
);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}