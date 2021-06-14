using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using AirLiquide.Application.v1.Exceptions;
using AirLiquide.Application.v1.Mediator;
using AirLiquide.Model;
using AirLiquide.Repository;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AirLiquide.Application.Test
{
    public class GetCustomerByIdMediatorTest
    {
        private Mock<ICustomerRepository> _repository;

        public GetCustomerByIdMediatorTest()
        {
            _repository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task WhenCustomerIsInvalidShouldThrowCustomValidationException()
        {
            var validator = new GetCustomerByIdRequestValidator();
            var request = new GetCustomerByIdRequest();
            validator.TestValidate(request);

            var mediator = new GetCustomerByIdMediator(validator, _repository.Object);

            await Assert.ThrowsAsync<CustomValidationException>(() => mediator.Handle(request, default));
        }

        [Fact]
        public async Task WhenCustomerIsVvalidShouldReturnCustomer()
        {
            var customerId = Guid.NewGuid();
            var validator = new GetCustomerByIdRequestValidator();
            var request = new GetCustomerByIdRequest { Id = customerId };
            validator.TestValidate(request);

            var GetCustomerById = new Customer(customerId, "Name", 80);

            _repository.Setup(r => r.Get(It.IsAny<Guid>()).Result).Returns(GetCustomerById);

            var mediator = new GetCustomerByIdMediator(validator, _repository.Object);

            var response = await mediator.Handle(request, default);

            Assert.Equal(GetCustomerById.Id, response.Id);
            Assert.Equal(GetCustomerById.Name, response.Name);
            Assert.Equal(GetCustomerById.Age, response.Age);
        }


        [Fact]
        public async Task WhenCustomerNotFoundShouldThrowCustomNotFoundException()
        {
            var validator = new GetCustomerByIdRequestValidator();
            var request = new GetCustomerByIdRequest { Id = Guid.NewGuid() };
            validator.TestValidate(request);


            _repository.Setup(r => r.Insert(It.IsAny<Customer>()).Result).Returns(null);

            var mediator = new GetCustomerByIdMediator(validator, _repository.Object);
            await Assert.ThrowsAsync<CustomNotFoundException>(() => mediator.Handle(request, default));
        }


    }







    public class ListCustomerMediatorTest
    {
        private Mock<ICustomerRepository> _repository;

        public ListCustomerMediatorTest()
        {
            _repository = new Mock<ICustomerRepository>();
        }


        [Fact]
        public async Task WhenCustomerIsVvalidShouldReturnListOfCustomer()
        {

            var request = new ListCustomerRequest();

            var customer1Id = Guid.NewGuid();
            var customer2Id = Guid.NewGuid();
            var customerList = new List<Customer>
            {
                new Customer(customer1Id, "Name", 80),
                new Customer(customer2Id, "Name2", 10),
            }; 

            _repository.Setup(r => r.List().Result).Returns(customerList);

            var mediator = new ListCustomerMediator(_repository.Object);

            var response = await mediator.Handle(request, default);

            Assert.Equal(2, response.Count());

            var customer1 = response.First(c => c.Id == customer1Id);
            Assert.Equal("Name", customer1.Name);
            Assert.Equal(80, customer1.Age);

            var customer2 = response.First(c => c.Id == customer2Id);
            Assert.Equal("Name2", customer2.Name);
            Assert.Equal(10, customer2.Age);

            /*
            Assert.Equal(80, response[0].Age);
            Assert.Equal(10, response[1].Age);
            */
        }


        [Fact]
        public async Task WhenCustomerNotFoundShouldThrowCustomNotFoundException()
        {
            var request = new ListCustomerRequest();

            var customerList = new List<Customer>();
            _repository.Setup(r => r.List().Result).Returns(customerList);

            var mediator = new ListCustomerMediator(_repository.Object);
            await Assert.ThrowsAsync<CustomNotFoundException>(() => mediator.Handle(request, default));
        }


    }
}
