using System.Collections.Generic;
using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Services.Interfaces;

namespace CursoOnline.Dominio.Services
{
    public class BuscadorDeCurso : IBuscadorDeCurso
    {
        private readonly ICursoRepository _repository;
        private readonly CursoAdapter _adapter;

        public BuscadorDeCurso(ICursoRepository repository)
        {
            _repository = repository;
            _adapter = new CursoAdapter();
        }

        public async Task<CursoDTO> BuscarPorId(int id)
        {
            var curso = await _repository.BuscarPorId(id);

            return _adapter.Parse(curso);
        }

        public async Task<IEnumerable<CursoDTO>> BuscarTodos()
        {
            var cursos = await _repository.BuscarTodos();

            return _adapter.Parse(cursos);
        }

        public Task<bool> RegistroExiste(int? id)
        {
            return _repository.RegistroExiste(id);
        }
    }
}