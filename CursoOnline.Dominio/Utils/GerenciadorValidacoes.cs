using System.Collections.Generic;
using System.Linq;
using CursoOnline.Dominio.Exceptions;

namespace CursoOnline.Dominio.Utils
{
    public class GerenciadorValidacoes
    {
        private readonly IList<string> _mensagens;

        private GerenciadorValidacoes()
        { 
            _mensagens = new List<string>();
        }

        public static GerenciadorValidacoes Novo()
        {
            return new GerenciadorValidacoes();
        }

        public GerenciadorValidacoes Quando(bool condicaoPraErro, string mensagemErro)
        {
            if (condicaoPraErro)
            {
                _mensagens.Add(mensagemErro);
            }
            return this;
        }

        public void LancarExceptionSeExistir()
        {
            if (_mensagens.Any())
            {
                throw new ModeloInvalidoException(_mensagens);
            }
        }
    }
}