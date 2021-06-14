using MediatR;
using System.Collections.Generic;

namespace AirLiquide.Application.v1.ControllerMessage
{
    public class ListCustomerRequest : IRequest<IEnumerable<ListCustomerResponse>>
    {

    }
}
