using System.Threading.Tasks;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Services.Interfaces;

namespace CursoOnline.Dominio.Services
{
    public class RemovedorDeCurso : IRemovedorDeCurso
    {
        private readonly ICursoRepository _repository;

        public RemovedorDeCurso(ICursoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Remover(int id)
        {
            var curso = await _repository.BuscarPorId(id);
            _repository.Excluir(curso);
            await _repository.Commit();
            return true;
        }
    }
}