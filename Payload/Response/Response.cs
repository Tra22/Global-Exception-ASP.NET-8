using Microsoft.AspNetCore.Mvc;

namespace SampleExceptionHandler.Payload.Response{
    public class Response : ProblemDetails {
        public IDictionary<string, object?>? Data { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }
    }
}