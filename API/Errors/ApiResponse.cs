
using Microsoft.AspNetCore.Mvc;

namespace API.Errors
{
    public class ApiResponse
    {
       public int StatusCode {get;set;}
        public string Message {get;set;}
        public ApiResponse(int statusCode, string message = null) 
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefailtMessageForStatusCode(statusCode) ;
               
        }

        private string GetDefailtMessageForStatusCode(int statusCode)
        {
            return statusCode  switch {
                400 => "A badr request",
                401 => "Authorized,you are not",
                404 => "Resource found,it was not",
                500 => "error patch",
                _ => null
            };
        }


    }
}