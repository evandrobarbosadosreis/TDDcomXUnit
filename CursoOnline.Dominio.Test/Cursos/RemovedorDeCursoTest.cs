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
    public class RemovedorDeCursoTest
    {
        [Fact]
        public async Task DeveRemoverCurso()
        {
            //Given
            var faker   = new Faker();
            var repo    = new Mock<ICursoRepository>();
            var cursoId = faker.Random.Int(1, 1500);
            var curso   = CursoBuilder
                .CriarNovo()
                .ComId(cursoId)
                .Build();
            repo.Setup(r => r.BuscarPorId(cursoId)).ReturnsAsync(curso);
            var removedor = new RemovedorDeCurso(repo.Object);
            
            //When
            await removedor.Remover(cursoId);

            //Then
            repo.Verify(r => r.Excluir(It.Is<Curso>(curso => curso.Id == cursoId)));
        }
    }
}