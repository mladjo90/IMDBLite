using IMDBLite.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.API.BLL.RegistrationHelper
{
    public static class BLLRegistrationServices
    {
        public static void RegisterRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ICastRepository, CastRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ICastInMovieRepository, CastInMovieRepository>();
        }
    }
}
