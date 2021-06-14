using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using AirLiquide.Application.v1.Exceptions;
using AirLiquide.Application.v1.Mediator;
using AirLiquide.Repository;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class DeleteCustomerMediatorTest
    {
        private Mock<ICustomerRepository> _repository;

        public DeleteCustomerMediatorTest()
        {
            _repository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task WhenCustomerIsInvalidShouldThrowCustomValidationException()
        {
            var validator = new DeleteCustomerRequestValidator();
            var request = new DeleteCustomerRequest();
            validator.TestValidate(request);

            var mediator = new DeleteCustomerMediator(validator, _repository.Object);

            await Assert.ThrowsAsync<CustomValidationException>(() => mediator.Handle(request, default));
        }

        [Fact]
        public async Task WhenCustomerIsVvalidShouldDeleteCustomer()
        {
            var validator = new DeleteCustomerRequestValidator();
            var request = new DeleteCustomerRequest { Id = Guid.NewGuid()};
            validator.TestValidate(request);


            _repository.Setup(r => r.Delete(request.Id).Result).Returns(1);

            var mediator = new DeleteCustomerMediator(validator, _repository.Object);

            var response = await mediator.Handle(request, default);

            _repository.Verify(r => r.Delete(request.Id));
        }


        [Fact]
        public async Task WhenCustomerNoRowAffectedShouldThrowCustomNoRowsAffectedException()
        {
            var validator = new DeleteCustomerRequestValidator();
            var request = new DeleteCustomerRequest { Id = Guid.NewGuid()};
            validator.TestValidate(request);


            _repository.Setup(r => r.Delete(It.IsAny<Guid>()).Result).Returns(0);

            var mediator = new DeleteCustomerMediator(validator, _repository.Object);
            await Assert.ThrowsAsync<CustomNoRowsAffectedException>(() => mediator.Handle(request, default));
        }


    }
}
