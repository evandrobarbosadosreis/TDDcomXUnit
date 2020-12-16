using System;
using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Models;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Dominio.Utils;
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
        public void DeveCriarCorretamente()
        {
            //Given
            var nome         = _faker.Random.Word();
            var descricao    = _faker.Lorem.Paragraph();
            var cargaHoraria = _faker.Random.Int(1, 180);
            var valor        = _faker.Random.Decimal(0.01m, 1000m);
            var publicoAlvo  = _faker.Random.Enum<EPublicoAlvo>();

            //When
            var curso = new Curso(
                nome,
                descricao,
                cargaHoraria,
                publicoAlvo,
                valor);

            //Then
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(descricao, curso.Descricao);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(valor, curso.Valor);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarComNomeInvalido(string nomeInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .CriarNovo()
                .ComNome(nomeInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCriarComCargaHorariaInvalida(int cargaHorariaInvalida)
        {
            //When
            Action action = () => CursoBuilder
                .CriarNovo()
                .ComCargaHoraria(cargaHorariaInvalida)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0.00)]
        [InlineData(-0.01)]
        [InlineData(-100.00)]
        public void NaoDeveCriarComPrecoInvalido(decimal valorInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .CriarNovo()
                .ComValor(valorInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.ValorInvalido);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void NaoDeveCriarComPublicoAlvoInvalido(int publicoAlvoInvalido)
        {
            //When
            Action action = () => CursoBuilder
                .CriarNovo()
                .ComPublicoAlvo((EPublicoAlvo) publicoAlvoInvalido)
                .Build();

            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.PublicoAlvoInvalido);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            //Given
            var novoNome = _faker.Random.Word();
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
            curso.AlterarNome(novoNome);

            //Then
            Assert.Equal(novoNome, curso.Nome);
        }

        [Fact]
        public void DeveAlterarDescricao()
        {
            //Given
            var novaDescricao = _faker.Lorem.Paragraph();
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
            curso.AlterarDescricao(novaDescricao);

            //Then
            Assert.Equal(novaDescricao, curso.Descricao);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            //Given
            var novoValor = _faker.Random.Decimal(0.01m, 1500m);
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
            curso.AlterarValor(novoValor);

            //Then
            Assert.Equal(novoValor, curso.Valor);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            //Given
            var novaCargaHoraria = _faker.Random.Int(1, 180);
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
            curso.AlterarCargaHoraria(novaCargaHoraria);

            //Then
            Assert.Equal(novaCargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void DeveAlterarPublicoAlvo()
        {
            //Given
            var novoPublicoAlvo = _faker.Random.Enum<EPublicoAlvo>();
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
           curso.AlterarPublicoAlvo(novoPublicoAlvo);  

            //Then
            Assert.Equal(novoPublicoAlvo, curso.PublicoAlvo);
        }        

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            //Given
            var curso = CursoBuilder
                .CriarNovo()
                .Build();
            
            //When
            Action action = () => curso.AlterarNome(nomeInvalido);
            
            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.NomeInvalido);
        }        

        [Theory]
        [InlineData(0.0)]
        [InlineData(-0.01)]
        [InlineData(-100.00)]
        public void NaoDeveAlterarComValorInvalido(decimal valorInvalido)
        {
            //Given
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
            Action action = () => curso.AlterarValor(valorInvalido);

            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.ValorInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveAlterarComCargaHorariaInvalida(int cargaHorariaInvalida)
        {            
            //Given
            var curso = CursoBuilder
                .CriarNovo()
                .Build();
            
            //When
            Action action = () => curso.AlterarCargaHoraria(cargaHorariaInvalida);
            
            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void NaoDeveAlterarComPublicoAlvoInvalido(int publicoAlvoInvalido)
        {
            //Given
            var curso = CursoBuilder
                .CriarNovo()
                .Build();

            //When
            Action action = () => curso.AlterarPublicoAlvo((EPublicoAlvo) publicoAlvoInvalido);

            //Then
            Assert.Throws<ModeloInvalidoException>(action).ComMensagem(Resources.PublicoAlvoInvalido);
        }

    }

}