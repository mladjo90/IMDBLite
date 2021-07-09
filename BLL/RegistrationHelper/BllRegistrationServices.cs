using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBLite.BLL.Implementation;
using IMDBLite.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IMDBLite.BLL.RegistrationHelper
{
    public static class BllRegistrationServices
    {
        public static void RegistrationBLLDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMovieBLL, MovieBLL>();
            services.AddScoped<IRatingBLL, RatingBLL>();
        }
    }
}
