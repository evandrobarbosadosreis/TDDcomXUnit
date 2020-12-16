using System.Collections.Generic;
using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;

namespace CursoOnline.Dominio.Services.Interfaces
{
    public interface IBuscadorDeCurso
    {
        Task<CursoDTO> BuscarPorId(int id);
        Task<IEnumerable<CursoDTO>> BuscarTodos();
        Task<bool> RegistroExiste(int? id);
    }    
}