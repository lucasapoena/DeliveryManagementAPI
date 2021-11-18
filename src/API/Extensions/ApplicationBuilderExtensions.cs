using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }

        internal static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }

        internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
            => app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    }
}