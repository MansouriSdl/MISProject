using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DivisionEquipmentRequestFeatures.Queries;
using MIS.Application.Features.ServiceEquipmentRequestFeatures.Commands;
using MIS.Application.Features.ServiceEquipmentRequestFeatures.Queries;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceEquipmentRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceEquipmentRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetServiceEquipmentRequestByService/{userId}")]
        public async Task<ActionResult<IEnumerable<ServiceEquipmentRequestResponse>>> GetServiceEquipmentRequestByService(Guid userId, [FromBody] FilterRequest request)
        {
            var query = new GetServiceEquipmentRequestByService(userId, request);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }

        [HttpPost("PostServiceEquipmentRequest")]
        public async Task<ActionResult<MessageResponse>> PostServiceEquipmentRequest([FromBody] PostBureauEquipmentRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPost("GetPendingRequests")]
        public async Task<ActionResult<IEnumerable<ServiceEquipmentRequestResponse>>> GetPendingRequests([FromBody] FilterRequest request)
        {
            var query = new GetPenddingRequests(request);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }

    }
}
