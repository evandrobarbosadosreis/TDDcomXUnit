using System.Collections.Generic;
using System.Threading.Tasks;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Interfaces
{
    public interface IRepository<TEntidade> where TEntidade : Entidade
    {
        Task<bool> Salvar(TEntidade entidade);
        Task<bool> Atualizar(TEntidade entidade);
        Task<bool> Excluir(TEntidade entidade);
        ValueTask<TEntidade> BuscarPorId(int id);
        Task<List<TEntidade>> BuscarTodos();
    }
}