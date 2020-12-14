using System;
using CursoOnline.Dominio.Test.Extensions;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Curso
{
    public class CursoTest
    {
    
        private readonly string _nome;
        private readonly int _cargaHoraria;
        private readonly EPublicoAlvo _publicoAlvo;
        private readonly decimal _valor;

        public CursoTest()
        {
            _nome         = "Informática Básica";
            _cargaHoraria = 80;
            _publicoAlvo  = EPublicoAlvo.Estudante;
            _valor        = 950.25m;
        }

        
        [Fact(DisplayName = "DeveCriarCurso")]
        public void DeveCriarCurso()
        {
            //Given
            var cursoEsperado = new 
            {
                Nome         = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo  = _publicoAlvo,
                Valor        = _valor
            };

            //When
            var curso = new Curso(
                cursoEsperado.Nome, 
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
            Action action = () => new Curso(
                nomeInvalido, 
                _cargaHoraria, 
                _publicoAlvo, 
                _valor);
            
            //Then
            Assert.Throws<ArgumentException>(action).WithMessage("Nome inválido");
        }

        [Theory(DisplayName = "NaoDeveTerCargaHorariaInvalida")]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveTerCargaHorariaInvalida(int cargaHorariaInvalida)
        {
            //When
            Action action = () => new Curso(
                _nome, 
                cargaHorariaInvalida, 
                _publicoAlvo, 
                _valor);

            //Then
            Assert.Throws<ArgumentException>(action).WithMessage("Carga horária inválida");
        }

        [Theory(DisplayName = "NaoDeveTerPrecoInvalido")]
        [InlineData(0.00)]
        [InlineData(-0.01)]
        public void NaoDeveTerPrecoInvalido(decimal valorInvalido)
        {
            //When
            Action action = () => new Curso(
                _nome, 
                _cargaHoraria, 
                _publicoAlvo,
                valorInvalido);

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
        public int CargaHoraria { get; }
        public EPublicoAlvo PublicoAlvo { get; }
        public decimal Valor { get; }

        public Curso(string nome, int cargaHoraria, EPublicoAlvo publicoAlvo, decimal valor)
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
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}