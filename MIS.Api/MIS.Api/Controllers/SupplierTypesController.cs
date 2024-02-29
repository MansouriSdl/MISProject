using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.SupplierTypeFeatures.Commands;
using MIS.Application.Features.SupplierTypeFeatures.Queries;
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
    public class SupplierTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllSupplierTypes")]
        public async Task<ActionResult<IEnumerable<SupplierTypeResponse>>> GetAllSupplierTypes()
        {
            var query = new GetAllSupplierTypesRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("AddSupplierType")]
        public async Task<ActionResult<MessageResponse>> AddSupplierType([FromBody] SupplierTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }   

        [HttpPut("UpdateSupplierType")]
        public async Task<ActionResult<MessageResponse>> UpdateSupplierType([FromBody] UpdateSupplierTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("DeleteSupplierType")]
        public async Task<ActionResult<MessageResponse>> DeleteSupplierType([FromBody] DeleteSupplierTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

    }
}
