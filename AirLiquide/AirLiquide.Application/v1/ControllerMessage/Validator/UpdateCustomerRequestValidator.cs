using FluentValidation;

namespace AirLiquide.Application.v1.ControllerMessage.Validator
{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            Include(new CustomerBaseRequestValidator());
            RuleFor(c => c.Id).NotNull().NotEmpty();
        }
    }
}
