using System.Collections.Generic;
using System.Threading.Tasks;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Interfaces
{
    public interface IRepository<TEntidade> where TEntidade : Entidade
    {
        Task Adicionar(TEntidade entidade);
        void Excluir(TEntidade entidade);
        ValueTask<TEntidade> BuscarPorId(int? id);
        Task<IEnumerable<TEntidade>> BuscarTodos();
        Task<bool> RegistroExiste(int? id);
        Task<int> Commit();
    }
}