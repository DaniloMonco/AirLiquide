using AirLiquide.Application.v1.ControllerMessage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AirLiquide.Api.v1.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCustomerResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            return CreatedAtAction("Create", await _mediator.Send(request));
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Update(Guid customerId, UpdateCustomerRequest request)
        {
            request.Id = customerId;
            await _mediator.Send(request);
            return NoContent();
        }


        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            var request = new DeleteCustomerRequest { Id = customerId };
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCustomerByIdResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetById(Guid customerId)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdRequest { Id = customerId }));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListCustomerResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new ListCustomerRequest());
            return Ok(response);
        }


    }
}
