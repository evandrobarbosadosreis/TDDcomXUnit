using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;

namespace CursoOnline.Dominio.Interfaces
{
    public interface IAdicionarCursoCommand
    {
        Task<bool> Adicionar(CursoDTO dto);
    }    
}