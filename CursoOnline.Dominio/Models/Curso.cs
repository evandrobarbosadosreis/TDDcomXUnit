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
                .Quando(string.IsNullOrEmpty(nome), "Nome inválido")
                .Quando(cargaHoraria <= 0, "Carga horária inválida")
                .Quando(valor <= 0, "Valor inválido")
                .Quando(!Enum.IsDefined(publicoAlvo), "Público alvo inválido")
                .LancarExceptionSeExistir();

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}