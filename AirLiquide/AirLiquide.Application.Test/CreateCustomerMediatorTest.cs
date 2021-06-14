using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using AirLiquide.Application.v1.Exceptions;
using AirLiquide.Application.v1.Mediator;
using AirLiquide.Model;
using AirLiquide.Repository;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class CreateCustomerMediatorTest
    {
        private Mock<ICustomerRepository> _repository;

        public CreateCustomerMediatorTest()
        {
            _repository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task WhenCustomerIsInvalidShouldThrowCustomValidationException()
        {
            var validator = new CreateCustomerRequestValidator();
            var request = new CreateCustomerRequest();
            validator.TestValidate(request);

            var mediator = new CreateCustomerMediator(validator, _repository.Object);

            await Assert.ThrowsAsync<CustomValidationException>(() => mediator.Handle(request, default));
        }

        [Fact]
        public async Task WhenCustomerIsVvalidShouldReturnCustomerId()
        {
            var validator = new CreateCustomerRequestValidator();
            var request = new CreateCustomerRequest { Age = 10, Name = "Name" };
            validator.TestValidate(request);


            _repository.Setup(r => r.Insert(It.IsAny<Customer>()).Result).Returns(1);

            var mediator = new CreateCustomerMediator(validator, _repository.Object);

            var response = await mediator.Handle(request, default);

            Assert.NotEqual(Guid.Empty, response.Id);
        }


        [Fact]
        public async Task WhenCustomerNoRowAffectedShouldThrowCustomNoRowsAffectedException()
        {
            var validator = new CreateCustomerRequestValidator();
            var request = new CreateCustomerRequest { Age = 10, Name = "Name" };
            validator.TestValidate(request);


            _repository.Setup(r => r.Insert(It.IsAny<Customer>()).Result).Returns(0);

            var mediator = new CreateCustomerMediator(validator, _repository.Object);
            await Assert.ThrowsAsync<CustomNoRowsAffectedException>(() => mediator.Handle(request, default));
        }


    }
}
