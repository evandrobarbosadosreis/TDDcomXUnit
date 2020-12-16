using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Models;

namespace CursoOnline.Dominio.Test.Builders
{
    public class CursoBuilder
    {

        private int _id;
        private string _nome;
        private string _descricao;
        private int _cargaHoraria;
        private decimal _valor;
        private EPublicoAlvo _publicoAlvo;

        private CursoBuilder()
        {
            var faker = new Faker();
            _id           = 0;
            _nome         = faker.Random.Word();
            _descricao    = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Int(1, 180);
            _valor        = faker.Random.Decimal(0.01m, 1000m);
            _publicoAlvo  = faker.Random.Enum<EPublicoAlvo>();
        }

        public static CursoBuilder CriarNovo() => new CursoBuilder();

        public CursoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public CursoBuilder ComNome(string nome) 
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(int cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(EPublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(
                _nome,
                _descricao,
                _cargaHoraria,
                _publicoAlvo,
                _valor);

            if (_id > 0)
            {
                var prop = curso.GetType().GetProperty("Id");
                prop.SetValue(curso, _id);
            }

            return curso;
        }

    }
}