using CursoOnline.Dominio.Test.Cursos;

namespace CursoOnline.Dominio.Test.Builders
{
    public class CursoBuilder
    {

        private string _nome = "Informática Básica";
        private string _descricao = "";
        private int _cargaHoraria = 80;
        private decimal _valor = 950.25m;
        private EPublicoAlvo _publicoAlvo = EPublicoAlvo.Estudante;


        public static CursoBuilder Novo() => new CursoBuilder();

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

        public Curso Build()
        {
            return new Curso(
                _nome,
                _descricao,
                _cargaHoraria,
                _publicoAlvo,
                _valor);
        }

    }
}