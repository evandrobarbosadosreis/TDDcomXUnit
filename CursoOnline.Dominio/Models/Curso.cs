using System;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Models
{
    public class Curso : Entidade
    {
        public string Nome { get; }
        public string Descricao { get; }
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

            if (!Enum.IsDefined(publicoAlvo))
            {   
                throw new ArgumentException("Público alvo inválido");
            }

            Nome         = nome;
            Descricao    = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo  = publicoAlvo;
            Valor        = valor;
        }
    }
}