using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PreceptorTime.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTime.Api.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddPrecetorTimeContext(this IServiceCollection services, string connectionString)
        {
            return services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<PreceptorTimeContext>(contextOptions =>
                {
                    contextOptions.UseSqlServer(
                        connectionString,
                        serverOptions =>
                        {
                            serverOptions.MigrationsAssembly
                            (typeof(Startup).Assembly.FullName);
                        });
                });
        }
    }
}
