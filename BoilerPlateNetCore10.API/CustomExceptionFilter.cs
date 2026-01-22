using BoilerPlateNetCore10.Domain.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Serilog;

namespace BoilerPlateNetCore10.API
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {

        private readonly Serilog.ILogger _logger;

        public CustomExceptionFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
          
            ObjectResult result;
            switch(context.Exception)
            {
                case DomainExceptionValidation:
                    result = new ObjectResult(new
                    {
                        message = "A validation error occurred.",
                        detailedMessage = context.Exception.Message,
                        traceId = context.HttpContext.TraceIdentifier
                    })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;
                default:
                    result = new ObjectResult(new
                    {
                        message = "An internal server error occurred.",
                        detailedMessage = context.Exception.Message,
                        traceId = context.HttpContext.TraceIdentifier
                    })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    WriteToLogUnexpectedException(context.Exception);
                    break;
            }
                
            context.Result = result;
            context.ExceptionHandled = true; // Stops the exception from propagating further
        }

        private void WriteToLogUnexpectedException(Exception ex)
        {
            Log.Logger = _logger;
            Log.Error(ex.Message);
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                Log.Error(innerException.Message);
                innerException = innerException.InnerException;
            }            
        }

    }
}
