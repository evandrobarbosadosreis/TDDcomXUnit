using System;
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
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDTO _cursoDTO;
        private readonly Mock<ICursoRepository> _repository;
        private readonly ArmazenadorDeCurso _armazenador;

        public ArmazenadorDeCursoTest()
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
            _armazenador = new ArmazenadorDeCurso(_repository.Object);            
        }


        [Fact]
        public void DeveAdicionarNovoCurso()
        {            
            //When
            _armazenador.Armazenar(_cursoDTO);
            
            //Then
            _repository.Verify(r => r.Adicionar(It.Is<Curso>(curso => 
                curso.Nome == _cursoDTO.Nome && 
                curso.Descricao == _cursoDTO.Descricao && 
                curso.Valor == _cursoDTO.Valor &&
                curso.CargaHoraria == _cursoDTO.CargaHoraria &&
                curso.PublicoAlvo == _cursoDTO.PublicoAlvo
            ))); 
        }

        [Fact(DisplayName = "NaoDeveSalvarCursoComMesmoNome")]
        public void NaoDeveSalvarCursoComMesmoNome()
        {
            //Given
            var nomeCursoRepetido = _cursoDTO.Nome;
            var cursoJaCadastrado = CursoBuilder
                .Novo()
                .ComNome(nomeCursoRepetido)
                .Build();
            _repository.Setup(repo => repo.BuscarPorNome(nomeCursoRepetido)).Returns(cursoJaCadastrado);

            //When
            Action action = () => _armazenador.Armazenar(_cursoDTO);
            
            //Then
            Assert.Throws<ArgumentException>(action).WithMessage("Já existe um curso cadastrado com esse nome");
        }
    }



}