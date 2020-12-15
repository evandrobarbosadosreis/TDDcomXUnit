using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Models;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {

        [Fact]
        public void DeveAdicionarNovoCurso()
        {
            //Given
            var fake = new Faker();
            var cursoDTO = new CursoDTO
            {
                Nome         = fake.Random.Word(),
                Descricao    = fake.Lorem.Paragraph(),
                Valor        = fake.Random.Decimal(0.01m, 1000m),
                CargaHoraria = fake.Random.Int(1, 180),
                PublicoAlvo  = fake.Random.Enum<EPublicoAlvo>()
            };
            var repository  = new Mock<ICursoRepository>();
            var armazenador = new ArmazenadorDeCurso(repository.Object);
            
            //When
            armazenador.Armazenar(cursoDTO);
            
            //Then
            repository.Verify(r => r.Adicionar(It.Is<Curso>(curso => 
                curso.Nome == cursoDTO.Nome && 
                curso.Descricao == cursoDTO.Descricao && 
                curso.Valor == cursoDTO.Valor &&
                curso.CargaHoraria == cursoDTO.CargaHoraria &&
                curso.PublicoAlvo == cursoDTO.PublicoAlvo
            ))); 
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CargaHoraria { get; set; }
        public EPublicoAlvo PublicoAlvo { get; set; }
    }

    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepository _repository;

        public ArmazenadorDeCurso(ICursoRepository repository)
        {
            _repository = repository;
        }

        public void Armazenar(CursoDTO dto)
        {
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