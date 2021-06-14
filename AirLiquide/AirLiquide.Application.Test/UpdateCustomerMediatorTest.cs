using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using AirLiquide.Application.v1.Exceptions;
using AirLiquide.Application.v1.Mediator;
using AirLiquide.Model;
using AirLiquide.Repository;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class UpdateCustomerMediatorTest
    {
        private Mock<ICustomerRepository> _repository;

        public UpdateCustomerMediatorTest()
        {
            _repository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task WhenCustomerIsInvalidShouldThrowCustomValidationException()
        {
            var validator = new UpdateCustomerRequestValidator();
            var request = new UpdateCustomerRequest();
            validator.TestValidate(request);

            var mediator = new UpdateCustomerMediator(validator, _repository.Object);

            await Assert.ThrowsAsync<CustomValidationException>(() => mediator.Handle(request, default));
        }

        [Fact]
        public async Task WhenCustomerIdIsEmptyShouldThrowCustomValidationException()
        {
            var validator = new UpdateCustomerRequestValidator();
            var request = new UpdateCustomerRequest();
            request.Name = "Name";
            request.Age = 10;
            request.Id = Guid.Empty;

            validator.TestValidate(request);

            var mediator = new UpdateCustomerMediator(validator, _repository.Object);

            await Assert.ThrowsAsync<CustomValidationException>(() => mediator.Handle(request, default));
        }

        [Fact]
        public async Task WhenCustomerIsVvalidShouldReturnCustomerId()
        {
            var validator = new UpdateCustomerRequestValidator();
            var customerId = Guid.NewGuid();
            var request = new UpdateCustomerRequest { Id = customerId,  Age = 10, Name = "Name" };
            validator.TestValidate(request);

            Customer customerUpdated = null;

            _repository.Setup(r => r.Update(It.IsAny<Customer>()).Result)
                .Returns(1)
                .Callback((Customer obj) => customerUpdated = new Customer(obj.Id, obj.Name, obj.Age));

            var mediator = new UpdateCustomerMediator(validator, _repository.Object);

            var response = await mediator.Handle(request, default);

            _repository.Verify(r => r.Update(It.IsAny<Customer>()));
            Assert.Equal(request.Age, customerUpdated.Age);
            Assert.Equal(request.Id, customerUpdated.Id);
            Assert.Equal(request.Name, customerUpdated.Name);
        }


        [Fact]
        public async Task WhenCustomerNoRowAffectedShouldThrowCustomNoRowsAffectedException()
        {
            var validator = new UpdateCustomerRequestValidator();
            var request = new UpdateCustomerRequest { Id = Guid.NewGuid(), Age = 10, Name = "Name" };
            validator.TestValidate(request);


            _repository.Setup(r => r.Update(It.IsAny<Customer>()).Result).Returns(0);

            var mediator = new UpdateCustomerMediator(validator, _repository.Object);
            await Assert.ThrowsAsync<CustomNoRowsAffectedException>(() => mediator.Handle(request, default));
        }


    }
}
