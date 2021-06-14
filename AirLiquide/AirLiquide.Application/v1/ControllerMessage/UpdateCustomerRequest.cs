using MediatR;
using System;
using System.Text.Json.Serialization;

namespace AirLiquide.Application.v1.ControllerMessage
{
    public class UpdateCustomerRequest : CustomerBaseRequest, IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
