using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.EmployeeTypeFeatures.Commands;
using MIS.Application.Features.EmployeeTypeFeatures.Queries;
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
    public class EmployeeTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllEmployeeTypes")]
        public async Task<ActionResult<IEnumerable<EmployeeTypeResponse>>> GetAllEmployeeTypes()
        {
            var query = new GetAllEmployeeTypesRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("AddEmployeeType")]
        public async Task<ActionResult<MessageResponse>> AddEmployeeType([FromBody] EmployeeTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("UpdateEmployeeType")]
        public async Task<ActionResult<MessageResponse>> UpdateEmployeeType([FromBody] UpdateEmployeeTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("DeleteEmployeeType")]
        public async Task<ActionResult<MessageResponse>> DeleteEmployeeType([FromBody] DeleteEmployeeTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
