using System;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Models
{
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