using FluentValidation;

namespace SampleExceptionHandler.Payload.Request{
    public class RegisterCustomerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public class Validator : AbstractValidator<RegisterCustomerRequest>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }
    }
}