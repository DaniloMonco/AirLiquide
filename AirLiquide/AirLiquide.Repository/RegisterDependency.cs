using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLiquide.Repository
{
    public static class RegisterDependency
    {
        public static void RegisterRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("AirLiquide")));

            services.AddTransient<ICustomerRepository, CustomerRepository>();


        }
    }
}
