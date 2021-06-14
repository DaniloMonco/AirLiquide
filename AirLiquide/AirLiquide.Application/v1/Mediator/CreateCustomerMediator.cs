using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.Exceptions;
using AirLiquide.Model;
using AirLiquide.Repository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirLiquide.Application.v1.Mediator
{
    public class CreateCustomerMediator : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        private readonly IValidator<CreateCustomerRequest> _validator;
        private readonly ICustomerRepository _repository;

        public CreateCustomerMediator(IValidator<CreateCustomerRequest> validator, ICustomerRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            await Validate(request, cancellationToken);

            var customerId = await InsertCustomer(request);
            return new CreateCustomerResponse { Id = customerId };

        }

        private async Task<Guid> InsertCustomer(CreateCustomerRequest request)
        {
            var customer = new Customer(Guid.NewGuid(), request.Name, request.Age);
            var rowsAffected = await _repository.Insert(customer);

            if (rowsAffected is 0)
                throw new CustomNoRowsAffectedException();
            return customer.Id;
        }

        private async Task Validate(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
                throw new CustomValidationException(result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
