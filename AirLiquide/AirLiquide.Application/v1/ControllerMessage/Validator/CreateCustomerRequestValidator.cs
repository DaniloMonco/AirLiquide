using FluentValidation;

namespace AirLiquide.Application.v1.ControllerMessage.Validator
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            Include(new CustomerBaseRequestValidator());
        }
    }
}
