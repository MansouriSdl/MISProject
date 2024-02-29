using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.ConsumptionTypeFeatures.Commands;
using MIS.Application.Features.ConsumptionTypeFeatures.Queries;
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
    public class ConsumptionTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsumptionTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllConsumptionTypes")]
        public async Task<ActionResult<IEnumerable<ConsumptionTypeResponse>>> GetAllConsumptionTypes()
        {
            var query = new GetAllConsumptionTypesRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("AddConsumptionType")]
        public async Task<ActionResult<MessageResponse>> AddConsumptionType([FromBody] ConsumptionTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("UpdateConsumptionType")]
        public async Task<ActionResult<MessageResponse>> UpdateConsumptionType([FromBody] UpdateConsumptionTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("DeleteConsumptionType")]
        public async Task<ActionResult<MessageResponse>> DeleteConsumptionType([FromBody] DeleteConsumptionTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
