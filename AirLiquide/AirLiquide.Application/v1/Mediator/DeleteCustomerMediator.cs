using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.Exceptions;
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
    public class DeleteCustomerMediator : IRequestHandler<DeleteCustomerRequest, Unit>
    {
        private readonly ICustomerRepository _repository;
        private readonly IValidator<DeleteCustomerRequest> _validator;

        public DeleteCustomerMediator(IValidator<DeleteCustomerRequest> validator, ICustomerRepository repository)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            await Validate(request, cancellationToken);
            await DeleteCustomer(request.Id);
            return Unit.Value;
        }

        private async Task Validate(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
                throw new CustomValidationException(result.Errors.Select(e => e.ErrorMessage));
        }

        private async Task DeleteCustomer(Guid customerId)
        {
            var rowsAffected = await _repository.Delete(customerId);
            if (rowsAffected is 0)
                throw new CustomNoRowsAffectedException();
        }
    }
}
