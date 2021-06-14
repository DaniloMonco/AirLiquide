using MediatR;
using System;

namespace AirLiquide.Application.v1.ControllerMessage
{
    public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
