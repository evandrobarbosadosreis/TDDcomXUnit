using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Utils;

namespace CursoOnline.Dominio.Models
{

    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public EPublicoAlvo PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }

        private Curso()
        { }

        public Curso(string nome, string descricao, int cargaHoraria, EPublicoAlvo publicoAlvo, decimal valor)
        {

            ValidacaoBuilder
                .CriarNovo()
                .SeEmBrancoOuNull(nome, Resources.NomeInvalido)
                .SeMenorOuIgualZero(cargaHoraria, Resources.CargaHorariaInvalida)
                .SeMenorOuIgualZero(valor, Resources.ValorInvalido)
                .SeEnumForInvalido(publicoAlvo, Resources.PublicoAlvoInvalido)
                .Build();

            Nome         = nome;
            Descricao    = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo  = publicoAlvo;
            Valor        = valor;
        }

        public void AlterarNome(string novoNome)
        {
            ValidacaoBuilder
                .CriarNovo()
                .SeEmBrancoOuNull(novoNome, Resources.NomeInvalido)
                .Build();
            
            Nome = novoNome;
        }

        public void AlterarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
        }

        public void AlterarValor(decimal novoValor)
        {
            ValidacaoBuilder
                .CriarNovo()
                .SeMenorOuIgualZero(novoValor, Resources.ValorInvalido)
                .Build();

            Valor = novoValor;
        }

        public void AlterarCargaHoraria(int novaCargaHoraria)
        {
            ValidacaoBuilder
                .CriarNovo()
                .SeMenorOuIgualZero(novaCargaHoraria,  Resources.CargaHorariaInvalida)
                .Build();

            CargaHoraria = novaCargaHoraria;
        }

        public void AlterarPublicoAlvo(EPublicoAlvo novoPublicoAlvo)
        {
            ValidacaoBuilder
                .CriarNovo()
                .SeEnumForInvalido(novoPublicoAlvo, Resources.PublicoAlvoInvalido)
                .Build();            

            PublicoAlvo = novoPublicoAlvo;
        }
    }
}