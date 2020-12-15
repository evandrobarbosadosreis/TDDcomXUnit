using System;
using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces;

namespace CursoOnline.Dominio.Services
{
    public class AdicionarCursoCommand : IAdicionarCursoCommand
    {
        private readonly ICursoRepository _repository;
        private readonly CursoAdapter _adapter;

        public AdicionarCursoCommand(ICursoRepository repository)
        {
            _repository = repository;
            _adapter    = new CursoAdapter();
        }

        public async Task<bool> Adicionar(CursoDTO dto)
        {
            var cursoJaCadastrado = await _repository.BuscarPorNome(dto.Nome);

            if (cursoJaCadastrado != null)
            {
                throw new ArgumentException("Já existe um curso cadastrado com esse nome");
            }

            var curso = _adapter.Parse(dto);
            var sucesso = await _repository.Salvar(curso);

            if (sucesso)
            {
                dto.Id = curso.Id;
            }

            return sucesso;
        }
    }
}