using System;
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
            GerenciadorValidacoes
                .Novo()
                .Quando(string.IsNullOrEmpty(nome), Resources.NomeInvalido)
                .Quando(cargaHoraria <= 0, Resources.CargaHorariaInvalida)
                .Quando(valor <= 0, Resources.ValorInvalido)
                .Quando(!Enum.IsDefined(publicoAlvo), Resources.PublicoAlvoInvalido)
                .LancarExceptionSeExistir();

            Nome         = nome;
            Descricao    = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo  = publicoAlvo;
            Valor        = valor;
        }

        public void AlterarNome(string novoNome)
        {
            GerenciadorValidacoes
                .Novo()
                .Quando(String.IsNullOrEmpty(novoNome), Resources.NomeInvalido)
                .LancarExceptionSeExistir();
            
            Nome = novoNome;
        }

        public void AlterarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
        }

        public void AlterarValor(decimal novoValor)
        {
            GerenciadorValidacoes
                .Novo()
                .Quando(novoValor <= 0, Resources.ValorInvalido)
                .LancarExceptionSeExistir();

            Valor = novoValor;
        }

        public void AlterarCargaHoraria(int novaCargaHoraria)
        {
            GerenciadorValidacoes
                .Novo()
                .Quando(novaCargaHoraria <= 0,  Resources.CargaHorariaInvalida)
                .LancarExceptionSeExistir();

            CargaHoraria = novaCargaHoraria;
        }

        public void AlterarPublicoAlvo(EPublicoAlvo novoPublicoAlvo)
        {
            GerenciadorValidacoes
                .Novo()
                .Quando(!Enum.IsDefined(novoPublicoAlvo), Resources.PublicoAlvoInvalido)
                .LancarExceptionSeExistir();            

            PublicoAlvo = novoPublicoAlvo;
        }
    }
}