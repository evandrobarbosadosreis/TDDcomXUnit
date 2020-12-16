using System;
using System.Collections.Generic;
using System.Linq;
using CursoOnline.Dominio.Exceptions;

namespace CursoOnline.Dominio.Utils
{
    public class ValidacaoBuilder
    {
        private readonly IList<string> _mensagens;

        private ValidacaoBuilder()
        { 
            _mensagens = new List<string>();
        }

        public static ValidacaoBuilder CriarNovo()
        {
            return new ValidacaoBuilder();
        }

        public ValidacaoBuilder Se(bool condicaoPraErro, string mensagemErro)
        {
            if (condicaoPraErro)
            {
                _mensagens.Add(mensagemErro);
            }
            return this;
        }

        public ValidacaoBuilder SeEmBrancoOuNull(string valor, string mensagemErro)
        {
            Se(string.IsNullOrWhiteSpace(valor), mensagemErro);
            return this;
        }

        public ValidacaoBuilder SeMenorOuIgualZero(int valor, string mensagemErro)
        {
            Se(valor <= 0, mensagemErro);
            return this;
        }

        public ValidacaoBuilder SeMenorOuIgualZero(decimal valor, string mensagemErro)
        {
            Se(valor <= 0, mensagemErro);
            return this;
        }

        public ValidacaoBuilder SeEnumForInvalido<TEnum>(TEnum valor, string mensagemErro) where TEnum : Enum    
        {
            Se(!Enum.IsDefined(typeof(TEnum), valor), mensagemErro);
            return this;
        }

        public void Build()
        {
            if (_mensagens.Any())
            {
                throw new ModeloInvalidoException(_mensagens);
            }
        }
    }
}