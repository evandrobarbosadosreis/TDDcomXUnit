using System;
using System.Threading.Tasks;
using CursoOnline.Dominio.Exceptions;
using Xunit;

namespace CursoOnline.Dominio.Test.Extensions
{
    public static class AssertExtensions
    {
        public static void ComMensagem(this ModeloInvalidoException exception, string mensagemEsperada)
        {
            Assert.Contains(mensagemEsperada, exception.Mensagens);
        }

        public static async Task ComMensagemAsync(this Task<ModeloInvalidoException> exception, string mensagemEsperada)
        {
            Assert.Contains(mensagemEsperada, (await exception).Mensagens);
        }
        
    }
}