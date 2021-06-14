using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using FluentValidation.TestHelper;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class CreateCustomerRequestValidatorTest
    {
        private CreateCustomerRequestValidator _validator;
        public CreateCustomerRequestValidatorTest()
        {
            _validator = new CreateCustomerRequestValidator();
        }

        [Fact]
        public void WhenNameIsEmptyShouldHaveError()
        {
            var customer = new CreateCustomerRequest();
            customer.Age = 10;
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Name);

        }

        [Fact]
        public void WhenAgeIsZeroShouldHaveError()
        {
            var customer = new CreateCustomerRequest();
            customer.Age = 0;
            customer.Name = "Name";
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Age);
        }

        [Fact]
        public void WhenCustomerIsValidShouldNotHaveError()
        {
            var customer = new CreateCustomerRequest();
            customer.Age = 10;
            customer.Name = "Name";
            var result = _validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Age);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Name);
        }

    }
}
