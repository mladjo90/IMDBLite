using IMDBLite.API.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IMDBLite.API.Repository.RegistrationHelper
{
    public static class RepositoryRegistrationServices
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
