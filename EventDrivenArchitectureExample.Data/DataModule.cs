using EventDrivenArchitectureExample.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventDrivenArchitectureExample.Data
{
    public static class DataModule
    {
        public static void ConfigureDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("ConexaoLeadDatabase"));
            });
        }
    }
}
