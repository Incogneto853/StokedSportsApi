using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;
using Newtonsoft.Json;

namespace StokedSportsWebApi.GlobalUtilities
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private static ILogger _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task Invoke(HttpContext _context)
        {
            try
            {
                await _next(_context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(_context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext _context, Exception _exception)
        {
            ErrorDetails exception = new ErrorDetails();
            var stackTrace = String.Empty;
            Guid identifier = Guid.NewGuid();
            var exceptionType = _exception.GetType();
            exception.Identifier = identifier.ToString();
            exception.Message = _exception.Message;

            switch (exceptionType.Name)
            {
                case "ArgumentException":
                case "ArgumentNullException":
                case "AggregateException":
                case "DuplicateNameException":
                    exception.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case "KeyNotFoundException":
                    exception.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    exception.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            //we only log server errors and not just common errors we are expectting 
            _logger.Error(_exception, "{Identifier}", identifier);
            var result = System.Text.Json.JsonSerializer.Serialize(exception);
            _context.Response.ContentType = "application/json";
            _context.Response.StatusCode = (int)exception.StatusCode;
            return _context.Response.WriteAsync(result);
        }
    }
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Identifier { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
