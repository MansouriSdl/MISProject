using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.BureauStockAssignmentFeatures.Commands;
using MIS.Application.Features.BureauStockAssignmentFeatures.Queries;
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

    public class BureauStockAssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BureauStockAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBueauStockAssignments")]
        public async Task<ActionResult<List<BureauStockAssignmentsResponse>>> GetBueauStockAssignments(FilterRequest request)
        {
            var query = new GetBueauStockAssignmentsRequest(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("AddAssignment")]
        public async Task<ActionResult<MessageResponse>> AddAssignment([FromBody] AddAssignmentRequest request)
        {
            var result =  await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("UpdateAssignmentsAvailability")]
        public async Task<ActionResult<int>> UpdateAssignmentsAvailability(UpdateBureauStockAssignmentAvailabilityRequest request)
        {
            var result = await _mediator.Send(request);
            return (int)result != 0 ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetStockAssignmentsDetails")]
        public async Task<ActionResult<StockAssignmentsDetailsResponse>> GetStockAssignmentsDetails(FilterRequest request)
        {
            var query = new GetStockAssignmentsDetailsRequest(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
