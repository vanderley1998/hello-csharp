using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plans.Api
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public ErrorResponse InnerError { get; set; }
        public string[] Details { get; set; }
        public static ErrorResponse From(Exception e)
        {
            if(e == null)
            {
                return null;
            }
            return new ErrorResponse
            {
                Code = e.HResult,
                Message = e.Message,
                InnerError = From(e.InnerException)
            };
        }

        public static ErrorResponse FromModelState(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(m => m.Errors);
            return new ErrorResponse
            {
                Code = 100,
                Message = "There was an error in the request",
                Details = erros.Select(e => e.ErrorMessage).ToArray()
            };
        }
    }
}
