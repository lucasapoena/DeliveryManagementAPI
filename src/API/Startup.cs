using API.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;
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
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDatabase(_configuration);
            //services.AddDatabaseMemory();
            services.AddApplicationLayer();
            services.AddApplicationServices();
            services.AddApplicationRequests();
            services.AddRepositories();
            services.RegisterSwagger();
            services.AddControllers().AddValidators();
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
