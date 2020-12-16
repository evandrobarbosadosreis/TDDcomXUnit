using System;
using System.Threading.Tasks;
using Bogus;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Dominio.Utils;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDTO _cursoDTO;
        private readonly Mock<ICursoRepository> _repository;
        private readonly ArmazenadorDeCurso _armazenador;        
        private readonly Faker _fake;

        public ArmazenadorDeCursoTest()
        {   
            _fake = new Faker();

            _cursoDTO = new CursoDTO
            {
                Nome         = _fake.Random.Word(),
                Descricao    = _fake.Lorem.Paragraph(),
                Valor        = _fake.Random.Decimal(0.01m, 1000m),
                CargaHoraria = _fake.Random.Int(1, 180),
                PublicoAlvo  = _fake.Random.Enum<EPublicoAlvo>()
            };

            _repository  = new Mock<ICursoRepository>();
            _armazenador = new ArmazenadorDeCurso(_repository.Object);            
        }

        [Fact]
        public async Task DeveAdicionarNovoCurso()
        {            
            //When
            await _armazenador.Armazenar(_cursoDTO);
            
            //Then 
            // Atesta que o método 'Adicionar' do repositório foi chamado e
            // que o parâmetro salvo foi, de fato, o curso com as informações
            // do nosso DTO
            _repository.Verify(r => r.Adicionar(It.Is<Curso>(curso => 
                curso.Nome         == _cursoDTO.Nome && 
                curso.Descricao    == _cursoDTO.Descricao && 
                curso.Valor        == _cursoDTO.Valor &&
                curso.CargaHoraria == _cursoDTO.CargaHoraria &&
                curso.PublicoAlvo  == _cursoDTO.PublicoAlvo
            ))); 
        }

        [Fact]
        public async Task Deve_Alterar_Curso()
        {
            //Given
            var cursoId = _fake.Random.Int(1, 1500);
            var curso = CursoBuilder
                .CriarNovo()
                .ComId(cursoId)
                .Build();
            _repository.Setup(r => r.BuscarPorId(cursoId)).ReturnsAsync(curso);
            _cursoDTO.Id = cursoId;

            //When
            await _armazenador.Armazenar(_cursoDTO);
            
            //Then
            _repository.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);
            Assert.Equal(curso.Nome, _cursoDTO.Nome);
            Assert.Equal(curso.Descricao, _cursoDTO.Descricao);
            Assert.Equal(curso.Valor, _cursoDTO.Valor);
            Assert.Equal(curso.CargaHoraria, _cursoDTO.CargaHoraria);
            Assert.Equal(curso.PublicoAlvo, _cursoDTO.PublicoAlvo);
        }

        [Fact]
        public async Task NaoDevePersistirCursoQuandoJaHouverOutroComMesmoNome()
        {
            //Given
            var nomeCursoRepetido = _cursoDTO.Nome;
            var cursoId           = _fake.Random.Int(1, 1500);
            var cursoJaCadastrado = CursoBuilder
                .CriarNovo()
                .ComId(cursoId)
                .ComNome(nomeCursoRepetido)
                .Build();
            _repository.Setup(repo => repo.BuscarPorNome(nomeCursoRepetido)).ReturnsAsync(cursoJaCadastrado);

            //When
            Func<Task> action = async () => await _armazenador.Armazenar(_cursoDTO);
            
            //Then
            await Assert.ThrowsAsync<ModeloInvalidoException>(action).ComMensagemAsync(Resources.NomeJaCadastrado);
        }

    }
}