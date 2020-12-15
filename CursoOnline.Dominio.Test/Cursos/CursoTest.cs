using System;
using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Models;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Dominio.Utils;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest
    {

        private readonly Faker _faker;

        public CursoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarCursoCorretamente()
        {
            //Given
            var cursoEsperado = new
            {
                Nome         = _faker.Random.Word(),
                Descricao    = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Int(1, 180),
                Valor        = _faker.Random.Decimal(0.01m, 1000m),
                PublicoAlvo  = _faker.Random.Enum<EPublicoAlvo>()
            };

            //When
            var curso = new Curso(
                cursoEsperado.Nome,
                cursoEsperado.Descricao,
                cursoEsperado.CargaHoraria,
                cursoEsperado.PublicoAlvo,
                cursoEsperado.Valor);

            //Then
            cursoEsperado.ToExpectedObject().Matches(curso);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarComNomeInvalido(string nomeInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComNome(nomeInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCriarComCargaHorariaInvalida(int cargaHorariaInvalida)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComCargaHoraria(cargaHorariaInvalida)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0.00)]
        [InlineData(-0.01)]
        [InlineData(-100.00)]
        public void NaoDeveCriarComPrecoInvalido(decimal valorInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComValor(valorInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.ValorInvalido);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void NaoDeveCriarComPublicoAlvoInvalido(int publicoAlvoInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComPublicoAlvo((EPublicoAlvo) publicoAlvoInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.PublicoAlvoInvalido);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            //Given
            var novoNome = _faker.Random.Word();
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
            curso.AlterarNome(novoNome);

            //Then
            Assert.Equal(curso.Nome, novoNome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            //Given
            var curso = CursoBuilder
                .Novo()
                .Build();
            
            //When
            Action action = () => curso.AlterarNome(nomeInvalido);
            
            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarDescricao()
        {
            //Given
            var novaDescricao = _faker.Lorem.Paragraph();
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
            curso.AlterarDescricao(novaDescricao);

            //Then
            Assert.Equal(curso.Descricao, novaDescricao);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            //Given
            var novoValor = _faker.Random.Decimal(0.01m, 1500m);
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
            curso.AlterarValor(novoValor);

            //Then
            Assert.Equal(curso.Valor, novoValor);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-0.01)]
        [InlineData(-100.00)]
        public void NaoDeveAlterarComValorInvalido(decimal valorInvalido)
        {
            //Given
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
            Action action = () => curso.AlterarValor(valorInvalido);

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.ValorInvalido);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            //Given
            var novaCargaHoraria = _faker.Random.Int(1, 180);
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
            curso.AlterarCargaHoraria(novaCargaHoraria);

            //Then
            Assert.Equal(curso.CargaHoraria, novaCargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveAlterarComCargaHorariaInvalida(int cargaHorariaInvalida)
        {            
            //Given
            var curso = CursoBuilder
                .Novo()
                .Build();
            
            //When
            Action action = () => curso.AlterarCargaHoraria(cargaHorariaInvalida);
            
            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarPublicoAlvo()
        {
            //Given
            var novoPublicoAlvo = _faker.Random.Enum<EPublicoAlvo>();
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
           curso.AlterarPublicoAlvo(novoPublicoAlvo);  

            //Then
            Assert.Equal(curso.PublicoAlvo, novoPublicoAlvo);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void NaoDeveAlterarComPublicoAlvoInvalido(int publicoAlvoInvalido)
        {
            //Given
            var curso = CursoBuilder
                .Novo()
                .Build();

            //When
            Action action = () => curso.AlterarPublicoAlvo((EPublicoAlvo) publicoAlvoInvalido);
            
            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage(Resources.PublicoAlvoInvalido);
        }
    }
}