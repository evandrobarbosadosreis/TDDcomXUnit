using CursoOnline.Dominio.Interfaces;
using CursoOnline.Infra.Context;
using CursoOnline.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CursoOnline.Dominio.Services.Interfaces;
using CursoOnline.Dominio.Services;

namespace CursoOnline.Infra.Config
{
    public static class InfraConfigStartup
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<PostgreSQLContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IArmazenadorDeCurso, ArmazenadorDeCurso>();
            services.AddScoped<IBuscadorDeCurso, BuscadorDeCurso>();
            services.AddScoped<IRemovedorDeCurso, RemovedorDeCurso>();
        }
    }
}
