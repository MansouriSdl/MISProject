using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.BureauFeatures.Commands;
using MIS.Application.Features.BureauFeatures.Queries;
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
    public class BureausController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BureausController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("GetAllBureaus")]
        public async Task<ActionResult<IEnumerable<BureauResponse>>> GetAllBureaus()
        {
            var query = new GetAllBureausRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetBureauById/{id}")]
        public async Task<ActionResult<BureauResponse>> GetBureauById(Guid id)
        {
            var query = new GetBureauByIdRequest(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetBureausByServiceId/{serviceId}")]
        public async Task<ActionResult<IEnumerable<Bureau>>> GetBureausByServiceId(Guid serviceId)
        {
            var query = new GetBureausByServiceId(serviceId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }

        [HttpPost("AddBureau")]
        public async Task<ActionResult<MessageResponse>> AddBureau([FromBody] BureauRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        [HttpPut("UpdateBureau")]
        public async Task<ActionResult<MessageResponse>> UpdateBureau([FromBody] UpdateBureauRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        
        [HttpPut("DeleteBureau")]
        public async Task<ActionResult<MessageResponse>> DeleteBureau([FromBody] DeleteBureauRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
