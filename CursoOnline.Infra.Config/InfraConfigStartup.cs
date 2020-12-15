using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Services;
using CursoOnline.Infra.Context;
using CursoOnline.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CursoOnline.Infra.Config
{
    public static class InfraConfigStartup
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<PostgreSQLContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IAdicionarCursoCommand, AdicionarCursoCommand>();
        }
    }
}
