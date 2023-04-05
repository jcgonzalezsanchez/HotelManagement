using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HotelManagement.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name: "hotalmanagement",
                    new OpenApiInfo { Title = "Hotel Management API v1", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("hotalmanagement/swagger.json", "Hotel Management API v1");
                options.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
