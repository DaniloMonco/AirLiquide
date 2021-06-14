using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class DeleteCustomerRequestValidatorTest
    {
        private DeleteCustomerRequestValidator _validator;
        public DeleteCustomerRequestValidatorTest()
        {
            _validator = new DeleteCustomerRequestValidator();
        }

        [Fact]
        public void WhenIdIsEmptyShouldHaveError()
        {
            var customer = new DeleteCustomerRequest();
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Id);
        }

        [Fact]
        public void WhenIdIsValidShouldNotHaveError()
        {
            var customer = new DeleteCustomerRequest();
            customer.Id = Guid.NewGuid();
            var result = _validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Id);
        }

    }
}
