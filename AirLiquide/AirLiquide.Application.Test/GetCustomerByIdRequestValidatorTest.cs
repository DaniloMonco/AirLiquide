using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class GetCustomerByIdRequestValidatorTest
    {
        private GetCustomerByIdRequestValidator _validator;
        public GetCustomerByIdRequestValidatorTest()
        {
            _validator = new GetCustomerByIdRequestValidator();
        }

        [Fact]
        public void WhenIdIsEmptyShouldHaveError()
        {
            var customer = new GetCustomerByIdRequest();
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Id);
        }

        [Fact]
        public void WhenIdIsValidShouldNotHaveError()
        {
            var customer = new GetCustomerByIdRequest();
            customer.Id = Guid.NewGuid();
            var result = _validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Id);
        }

    }
}
