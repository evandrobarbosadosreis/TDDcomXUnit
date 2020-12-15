using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Dominio.Test.Extensions
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string expectedMessage)
        {
            Assert.Equal(exception.Message, expectedMessage);
        }

        public static async Task WithMessageAsync(this Task<ArgumentException> exceptionTask, string expectedMessage)
        {
            var exception = await exceptionTask;
            exception.WithMessage(expectedMessage);
        }        
    }
}