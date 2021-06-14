using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class UpdateCustomerRequestValidatorTest
    {
        private UpdateCustomerRequestValidator _validator;
        public UpdateCustomerRequestValidatorTest()
        {
            _validator = new UpdateCustomerRequestValidator();
        }

        [Fact]
        public void WhenNameIsEmptyShouldHaveError()
        {
            var customer = new UpdateCustomerRequest();
            customer.Age = 10;
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Name);

        }

        [Fact]
        public void WhenAgeIsZeroShouldHaveError()
        {
            var customer = new UpdateCustomerRequest();
            customer.Age = 0;
            customer.Name = "Name";
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Age);
        }

        [Fact]
        public void WhenIdIsEmptyShouldHaveError()
        {
            var customer = new UpdateCustomerRequest();
            customer.Age = 0;
            customer.Name = "Name";
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Id);
        }

        [Fact]
        public void WhenCustomerIsValidShouldNotHaveError()
        {
            var customer = new UpdateCustomerRequest();
            customer.Id = Guid.NewGuid();
            customer.Age = 10;
            customer.Name = "Name";
            var result = _validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Id);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Age);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Name);
        }

    }
}
