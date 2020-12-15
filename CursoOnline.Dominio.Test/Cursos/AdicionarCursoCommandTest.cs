using System;
using System.Threading.Tasks;
using Bogus;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensions;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class AdicionarCursoCommandTest
    {
        private readonly CursoDTO _cursoDTO;
        private readonly Mock<ICursoRepository> _repository;
        private readonly AdicionarCursoCommand _command;

        public AdicionarCursoCommandTest()
        {
            var fake = new Faker();
            
            _cursoDTO = new CursoDTO
            {
                Nome         = fake.Random.Word(),
                Descricao    = fake.Lorem.Paragraph(),
                Valor        = fake.Random.Decimal(0.01m, 1000m),
                CargaHoraria = fake.Random.Int(1, 180),
                PublicoAlvo  = fake.Random.Enum<EPublicoAlvo>()
            };

            _repository  = new Mock<ICursoRepository>();
            _command = new AdicionarCursoCommand(_repository.Object);            
        }


        [Fact]
        public async Task DeveAdicionarNovoCurso()
        {            
            //When
            await _command.Adicionar(_cursoDTO);
            
            //Then
            _repository.Verify(r => r.Salvar(It.Is<Curso>(curso => 
                curso.Nome == _cursoDTO.Nome && 
                curso.Descricao == _cursoDTO.Descricao && 
                curso.Valor == _cursoDTO.Valor &&
                curso.CargaHoraria == _cursoDTO.CargaHoraria &&
                curso.PublicoAlvo == _cursoDTO.PublicoAlvo
            ))); 
        }

        [Fact(DisplayName = "NaoDeveSalvarCursoComMesmoNome")]
        public async Task NaoDeveSalvarCursoComMesmoNome()
        {
            //Given
            var nomeCursoRepetido = _cursoDTO.Nome;
            var cursoJaCadastrado = CursoBuilder
                .Novo()
                .ComNome(nomeCursoRepetido)
                .Build();
            _repository.Setup(repo => repo.BuscarPorNome(nomeCursoRepetido)).ReturnsAsync(cursoJaCadastrado);

            //When
            Func<Task> action = async () => await _command.Adicionar(_cursoDTO);
            
            //Then
            await Assert.ThrowsAsync<ArgumentException>(action).WithMessageAsync("Já existe um curso cadastrado com esse nome");
        }
    }



}