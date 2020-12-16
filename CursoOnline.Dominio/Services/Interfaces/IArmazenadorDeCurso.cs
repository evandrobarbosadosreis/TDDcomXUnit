using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;

namespace CursoOnline.Dominio.Services.Interfaces
{
    public interface IArmazenadorDeCurso
    {
        Task<bool> Armazenar(CursoDTO dto);
    }    
}