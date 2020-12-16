using System.Threading.Tasks;

namespace CursoOnline.Dominio.Services.Interfaces
{
    public interface IRemovedorDeCurso
    {
        Task<bool> Remover(int id);
    }
}