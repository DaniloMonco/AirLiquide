using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.Exceptions;
using AirLiquide.Model;
using AirLiquide.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirLiquide.Application.v1.Mediator
{
    public class ListCustomerMediator : IRequestHandler<ListCustomerRequest, IEnumerable<ListCustomerResponse>>
    {
        private readonly ICustomerRepository _repository;

        public ListCustomerMediator(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ListCustomerResponse>> Handle(ListCustomerRequest request, CancellationToken cancellationToken)
        {
            var customers = await _repository.List();
            await Validate(customers, cancellationToken);

            return customers.Select(c => new ListCustomerResponse
                                    {
                                        Age = c.Age,
                                        Id = c.Id,
                                        Name = c.Name
                                    }
            );

        }

        private Task Validate(IEnumerable<Customer> customers, CancellationToken cancellationToken)
        {
            if (customers is null || !customers.Any())
                throw new CustomNotFoundException();

            return Task.CompletedTask;
        }

    }
}
