using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Services;
using CursoOnline.Infra.Context;
using CursoOnline.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Infra.Config
{
    public static class InfraConfigStartup
    {
        public static void AddInfraServices(this IServiceCollection services)
        {
            services.AddDbContext<InMemoryContext>(options => options.UseInMemoryDatabase("database"));
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IAdicionarCursoCommand, AdicionarCursoCommand>();
        }
    }
}
