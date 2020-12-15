using System;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Services
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepository _repository;

        public ArmazenadorDeCurso(ICursoRepository repository)
        {
            _repository = repository;
        }

        public void Armazenar(CursoDTO dto)
        {
            var cursoJaCadastrado = _repository.BuscarPorNome(dto.Nome);

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

            _repository.Adicionar(curso);
        }
    }
}