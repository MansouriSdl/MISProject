using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.ServiceFeatures.Commands;
using MIS.Application.Features.ServiceFeatures.Queries;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllServices")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServices()
        {
            var query = new GetAllServicesRequest();
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }
        [HttpGet("GetServicesByDivisionId/{divisionId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServicesByDivisionId(Guid divisionId)
        {
            var query = new GetServicesByDivisionId(divisionId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }

        [HttpGet("GetServiceIdByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServiceIdByUserId(Guid userId)
        {
            var query = new GetServiceIdByUserIdRequest(userId);
            var result = await _mediator.Send(query);
            return result != Guid.Empty ? Ok(result) : BadRequest();

        }
        [HttpPost("AddService")]
        public async Task<ActionResult<MessageResponse>> AddService([FromBody] ServiceRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
       

        [HttpPut("UpdateService")]
        public async Task<ActionResult<MessageResponse>> UpdateService([FromBody] UpdateServiceRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("DeleteService")]
        public async Task<ActionResult<MessageResponse>> DeleteService([FromBody] DeleteServiceRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
