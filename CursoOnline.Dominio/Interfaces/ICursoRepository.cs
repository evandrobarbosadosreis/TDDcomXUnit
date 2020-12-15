using System.Threading.Tasks;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Interfaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Task<Curso> BuscarPorNome(string nome);
    }
}