using FluentValidation;

namespace AirLiquide.Application.v1.ControllerMessage.Validator
{
    public class DeleteCustomerRequestValidator : AbstractValidator<DeleteCustomerRequest>
    {
        public DeleteCustomerRequestValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();
        }
    }
}
