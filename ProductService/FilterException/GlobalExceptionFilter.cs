using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CatalogApi.FilterException
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Detalla las excepciones no controladas en la aplicacion
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {            
            var exceptionName = context.Exception.GetType().Name;
            var message = context.Exception.Message;
            var validation = new
            {
                Title = exceptionName,
                Detail = message
            };
            var json = new
            {
                Data = validation
            };
            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.ExceptionHandled = true;            
        }
    }
}
