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
    public class UpdateCustomerMediator : IRequestHandler<UpdateCustomerRequest, Unit>
    {
        private readonly IValidator<UpdateCustomerRequest> _validator;
        private readonly ICustomerRepository _repository;

        public UpdateCustomerMediator(IValidator<UpdateCustomerRequest> validator, ICustomerRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            await Validate(request, cancellationToken);
            var customer = new Customer(request.Id, request.Name, request.Age);
            await UpdateCustomer(customer);
            return Unit.Value;
        }

        private async Task Validate(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
                throw new CustomValidationException(result.Errors.Select(e => e.ErrorMessage));
        }

        private async Task UpdateCustomer(Customer customer)
        {
            var rowsAffected = await _repository.Update(customer);
            if (rowsAffected is 0)
                throw new CustomNoRowsAffectedException();
        }
    }
}
