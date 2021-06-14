using MediatR;

namespace AirLiquide.Application.v1.ControllerMessage
{
    public class CreateCustomerRequest : CustomerBaseRequest, IRequest<CreateCustomerResponse>
    {

    }
}
