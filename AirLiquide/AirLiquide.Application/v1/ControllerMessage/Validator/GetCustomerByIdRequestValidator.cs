using FluentValidation;

namespace AirLiquide.Application.v1.ControllerMessage.Validator
{
    public class GetCustomerByIdRequestValidator : AbstractValidator<GetCustomerByIdRequest>
    {
        public GetCustomerByIdRequestValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();
        }
    }
}
