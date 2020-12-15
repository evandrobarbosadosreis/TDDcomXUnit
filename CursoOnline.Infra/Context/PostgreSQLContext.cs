using CursoOnline.Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Infra.Context
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Curso> Cursos { get; set; }
    }
}