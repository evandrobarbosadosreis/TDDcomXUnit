using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CursoOnline.Infra.Context
{
    /// <summary>
    /// Essa factory permite ao EF instanciar o DbContext em tempo de  
    /// desing para gerar e aplicar as migrations no banco de dados
    /// </summary>
    public class PostgreSQLContextFactory : IDesignTimeDbContextFactory<PostgreSQLContext>
    {
        public PostgreSQLContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=127.0.0.1 ;Port=5432; Database=cursoonline; User ID=postgres; Password=postgres;";
            var optionsBuilder = new DbContextOptionsBuilder<PostgreSQLContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new PostgreSQLContext(optionsBuilder.Options);
        }
    }
}