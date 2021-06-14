using MediatR;
using System;

namespace AirLiquide.Application.v1.ControllerMessage
{
    public class DeleteCustomerRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
