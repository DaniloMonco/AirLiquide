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
    public class GetCustomerByIdMediator : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly IValidator<GetCustomerByIdRequest> _validator;

        public GetCustomerByIdMediator(IValidator<GetCustomerByIdRequest> validator, ICustomerRepository repository)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            await Validate(request, cancellationToken);

            var model = await GetCustomer(request.Id);

            return new GetCustomerByIdResponse
            {
                Id = model.Id,
                Age = model.Age,
                Name = model.Name
            };

        }

        private async Task Validate(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
                throw new CustomValidationException(result.Errors.Select(e => e.ErrorMessage));
        }


        private async Task<Customer> GetCustomer(Guid id)
        {
            var model = await _repository.Get(id);

            if (model is null)
                throw new CustomNotFoundException();

            return model;
        }
    }
}
