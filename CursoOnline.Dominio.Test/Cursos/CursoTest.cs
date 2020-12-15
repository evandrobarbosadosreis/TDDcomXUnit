using System;
using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Models;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensions;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest
    {

        [Fact(DisplayName = "DeveCriarCurso")]
        public void DeveCriarCurso()
        {
            //Given
            var faker = new Faker();

            var cursoEsperado = new
            {
                Nome         = faker.Random.Word(),
                Descricao    = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Int(1, 180),
                Valor        = faker.Random.Decimal(0.01m, 1000m),
                PublicoAlvo  = faker.Random.Enum<EPublicoAlvo>()
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


        [Theory(DisplayName = "NaoDeveTerNomeInvalido")]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveTerNomeInvalido(string nomeInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComNome(nomeInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage("Nome inválido");
        }

        [Theory(DisplayName = "NaoDeveTerCargaHorariaInvalida")]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveTerCargaHorariaInvalida(int cargaHorariaInvalida)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComCargaHoraria(cargaHorariaInvalida)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage("Carga horária inválida");
        }

        [Theory(DisplayName = "NaoDeveTerPrecoInvalido")]
        [InlineData(0.00)]
        [InlineData(-0.01)]
        public void NaoDeveTerPrecoInvalido(decimal valorInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComValor(valorInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage("Valor inválido");
        }

        [Theory(DisplayName = "NaoDeveTerPublicoAlvoInvalido")]
        [InlineData(-1)]
        [InlineData(5)]
        public void NaoDeveTerPublicoAlvoInvalido(int publicoAlvoInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .Novo()
                .ComPublicoAlvo((EPublicoAlvo) publicoAlvoInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).WithMessage("Público alvo inválido");
        }
    }
}