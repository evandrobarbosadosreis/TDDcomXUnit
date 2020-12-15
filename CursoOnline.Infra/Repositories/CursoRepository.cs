using System.Threading.Tasks;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;
using CursoOnline.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Infra.Repositories
{
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(InMemoryContext context) : base(context)
        { }

        public Task<Curso> BuscarPorNome(string nome)
        {
            return _dataset.FirstOrDefaultAsync(c => c.Nome == nome);
        }
    }
}