using System.ComponentModel.DataAnnotations;

namespace SampleExceptionHandler.ExceptionHandler{
    public class CustomValidationException : ValidationException{
        public Dictionary<string, string[]> Errors {get;}
        public CustomValidationException(string message, Dictionary<string, string[]> Errors) : base(message){
            this.Errors = Errors;
        }
    }
}