using System;
using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Services
{
    public class AdicionarCursoCommand : IAdicionarCursoCommand
    {
        private readonly ICursoRepository _repository;

        public AdicionarCursoCommand(ICursoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Adicionar(CursoDTO dto)
        {
            var cursoJaCadastrado = await _repository.BuscarPorNome(dto.Nome);

            if (cursoJaCadastrado != null)
            {
                throw new ArgumentException("Já existe um curso cadastrado com esse nome");
            }

            var curso = new Curso(
                dto.Nome,
                dto.Descricao,
                dto.CargaHoraria,
                dto.PublicoAlvo,
                dto.Valor);

            return await _repository.Salvar(curso);
        }
    }
}