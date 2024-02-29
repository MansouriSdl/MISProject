using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DivisionFeatures.Commands;
using MIS.Application.Features.DivisionFeatures.Queries;
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

    public class DivisionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DivisionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllDivisions")]
        public async Task<ActionResult<IEnumerable<DivisionResponse>>> GetAllDivisions()
        {
            var query = new GetAllDivisions();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetDivisionIdByName/{name}")]
        public async Task<ActionResult<Guid>> GetDivisionIdByName(string name)
        {
            var query = new GetDivisionIdByNameRequest(name);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("AddDivision")]
        public async Task<ActionResult<MessageResponse>> AddDivision([FromBody] DivisionRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("UpdateDivision")]
        public async Task<ActionResult<MessageResponse>> UpdateDivision([FromBody] UpdateDivisionRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("DeleteDivision")]
        public async Task<ActionResult<MessageResponse>> DeleteDivision([FromBody] DeleteDivisionRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}

