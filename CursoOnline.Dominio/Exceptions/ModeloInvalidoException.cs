using System;
using System.Collections.Generic;

namespace CursoOnline.Dominio.Exceptions
{
    public class ModeloInvalidoException : ArgumentException
    {
        public ModeloInvalidoException(IEnumerable<string> mensagens) : base("Houveram falhas de validação de domínio")
        {
            Mensagens = mensagens;
        }

        public IEnumerable<string> Mensagens { get; private set; }
    }
}