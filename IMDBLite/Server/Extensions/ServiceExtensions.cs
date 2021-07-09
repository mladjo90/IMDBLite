using IMDBLite.API.DataModels.Helper;
using IMDBLite.API.Repository.RegistrationHelper;
using IMDBLite.BLL.RegistrationHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IMDBLite.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMySqlContext(configuration);
        }

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.RegisterRepositoryDependencies();
        }

        public static void ConfigureBLl(this IServiceCollection services)
        {
            services.RegistrationBLLDependencies();
        }
    }
}
