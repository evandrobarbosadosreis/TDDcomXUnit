using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Extensions
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string expectedMessage)
        {
            Assert.Equal(exception.Message, expectedMessage);
        }
    }
}