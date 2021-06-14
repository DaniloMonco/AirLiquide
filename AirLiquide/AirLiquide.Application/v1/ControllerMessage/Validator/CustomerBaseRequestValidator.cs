using FluentValidation;

namespace AirLiquide.Application.v1.ControllerMessage.Validator
{
    public class CustomerBaseRequestValidator : AbstractValidator<CustomerBaseRequest>
    {
        public CustomerBaseRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Age).GreaterThan(0);
        }
    }
}
