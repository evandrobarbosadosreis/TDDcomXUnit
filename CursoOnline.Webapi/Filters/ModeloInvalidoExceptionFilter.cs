using System.Net;
using CursoOnline.Dominio.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CursoOnline.Webapi.Filters
{
    public class ModeloInvalidoExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ModeloInvalidoException exception && ! context.ExceptionHandled)
            {
                context.Result = new BadRequestObjectResult(new 
                {
                    codigo    = HttpStatusCode.BadRequest,
                    mensagens = exception.Mensagens
                });
                context.ExceptionHandled = true;
            }
        }
    }
}