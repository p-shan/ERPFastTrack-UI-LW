using ERPFastTrack.APIModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.API.Internals.Middlewares
{

    
    
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError; // Default status code for unhandled exceptions

            // Customize handling based on the exception type
            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;
            else if (exception is ArgumentException)
                statusCode = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new BaseResponse { ErrorMessage = exception.Message + exception.StackTrace, Status = false } );
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
