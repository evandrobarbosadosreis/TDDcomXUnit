using System;
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

    public enum EPublicoAlvo
    {
        Estudante = 1,
        Universitario = 2,
        Empregado = 3,
        Empreendedor = 4
    }

    public class Curso
    {
        public string Nome { get; }
        private string Descricao { get; }
        public int CargaHoraria { get; }
        public EPublicoAlvo PublicoAlvo { get; }
        public decimal Valor { get; }

        public Curso(string nome, string descricao, int cargaHoraria, EPublicoAlvo publicoAlvo, decimal valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome inválido");
            }

            if (cargaHoraria <= 0)
            {
                throw new ArgumentException("Carga horária inválida");
            }

            if (valor <= 0)
            {
                throw new ArgumentException("Valor inválido");
            }

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}