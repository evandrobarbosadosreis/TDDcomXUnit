
using System.Threading.Tasks;
using CursoOnline.Dominio.Exceptions;
using Xunit;

namespace CursoOnline.Dominio.Test.Extensions
{
    public static class AssertExtension
    {
        public static void WithMessage(this ModeloInvalidoException exception, string expectedMessage)
        {
            Assert.Contains(expectedMessage, exception.Mensagens);
        }

        public static async Task WithMessageAsync(this Task<ModeloInvalidoException> exceptionTask, string expectedMessage)
        {
            var exception = await exceptionTask;
            exception.WithMessage(expectedMessage);
        }        
    }
}