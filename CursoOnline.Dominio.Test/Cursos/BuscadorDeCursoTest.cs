using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Test.Builders;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class BuscadorDeCursoTest
    {
        private readonly Faker _fake;
        private readonly Mock<ICursoRepository> _repository;
        private readonly BuscadorDeCurso _buscador;

        public BuscadorDeCursoTest()
        {
            _fake       = new Faker();
            _repository = new Mock<ICursoRepository>();
            _buscador   = new BuscadorDeCurso(_repository.Object);
        }

        private int GetCursoId()
        {
            return _fake.Random.Int(1, 15000);
        }

        [Fact]
        public async Task DeveBuscarTodos()
        {
            //Given
            var curso1 = CursoBuilder.CriarNovo().ComId(GetCursoId()).Build();
            var curso2 = CursoBuilder.CriarNovo().ComId(GetCursoId()).Build();
            var curso3 = CursoBuilder.CriarNovo().ComId(GetCursoId()).Build();
            var cursos = new List<Curso> { curso1, curso2, curso3 };
            _repository.Setup(r => r.BuscarTodos()).ReturnsAsync(cursos);

            //When
            var result = await _buscador.BuscarTodos();
            var lista  = result.ToList();
            
            //Then
            _repository.Verify(r => r.BuscarTodos());
            Assert.Equal(cursos.Count, lista.Count);
            Assert.Contains(result, c => c.Id == curso1.Id);
            Assert.Contains(result, c => c.Id == curso2.Id);
            Assert.Contains(result, c => c.Id == curso3.Id);
        }

        [Fact]
        public async Task DeveVerificarSeCursoExiste()
        {
            //Given
            var cursoId = GetCursoId();
            _repository.Setup(r => r.RegistroExiste(cursoId)).ReturnsAsync(true);

            //When
            var encontrou = await _buscador.RegistroExiste(cursoId);
            
            //Then
            _repository.Verify(r => r.RegistroExiste(cursoId));
            Assert.True(encontrou);
        }

        [Fact]
        public async Task DeveBuscarCursoPorId()
        {
            //Given
            var cursoId = GetCursoId();
            var curso = CursoBuilder
                .CriarNovo()
                .ComId(cursoId)
                .Build();
            _repository.Setup(r => r.BuscarPorId(cursoId)).ReturnsAsync(curso);

            //When
            var resultado = await _buscador.BuscarPorId(cursoId);
            
            //Then
            _repository.Verify(r => r.BuscarPorId(cursoId));
            Assert.Equal(cursoId, resultado.Id);
        }
    }
}