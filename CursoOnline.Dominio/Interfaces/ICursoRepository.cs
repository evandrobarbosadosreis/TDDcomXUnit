using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Interfaces
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        Curso BuscarPorNome(string nome);
    }
}