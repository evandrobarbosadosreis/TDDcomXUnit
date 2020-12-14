using System;
using CursoOnline.Dominio.Enums;
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
            var cursoEsperado = new
            {
                Nome = "Informática Básica",
                Descricao = "Exemplo de descrição",
                CargaHoraria = 80,
                PublicoAlvo = EPublicoAlvo.Estudante,
                Valor = 950.25m
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
            Assert.Throws<ArgumentException>(action).WithMessage("Nome inválido");
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
            Assert.Throws<ArgumentException>(action).WithMessage("Carga horária inválida");
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
            Assert.Throws<ArgumentException>(action).WithMessage("Valor inválido");
        }
    }
}