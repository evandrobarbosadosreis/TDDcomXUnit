using CursoOnline.Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Infra.Context
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        { }

        public DbSet<Curso> Cursos { get; set; }
    }
}