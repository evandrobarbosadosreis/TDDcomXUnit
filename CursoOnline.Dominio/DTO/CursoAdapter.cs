using System.Collections.Generic;
using System.Linq;
using CursoOnline.Dominio.DTO.Interfaces;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.DTO
{
    public class CursoAdapter : IAdapter<CursoDTO, Curso>, IAdapter<Curso, CursoDTO>
    {
        public Curso Parse(CursoDTO source)
        {
            var curso = new Curso(
                source.Nome, 
                source.Descricao, 
                source.CargaHoraria, 
                source.PublicoAlvo, 
                source.Valor);
            return curso;
        }

        public CursoDTO Parse(Curso source)
        {
            return new CursoDTO
            {
                Id           = source.Id,
                CargaHoraria = source.CargaHoraria,
                Descricao    = source.Descricao,
                Nome         = source.Nome,
                PublicoAlvo  = source.PublicoAlvo,
                Valor        = source.Valor
            };
        }
      
        public IEnumerable<CursoDTO> Parse(IEnumerable<Curso> source) => source.Select(Parse);

        public IEnumerable<Curso> Parse(IEnumerable<CursoDTO> source) => source.Select(Parse);  
    }
}