using API.Extensions;
using Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
           // services.AddDatabaseMemory();
            services.AddApplicationLayer();
            services.AddControllers();

            services.RegisterSwagger();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            app.UseExceptionHandling(env);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints();
            app.ConfigureSwagger();
        }
    }
}
