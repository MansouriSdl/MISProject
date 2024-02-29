using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.SupplierFeatures.Commands;
using MIS.Application.Features.SupplierFeatures.Queries;
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
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllSuppliers")]
        public async Task<ActionResult<IEnumerable<SupplierResponse>>> GetAllSuppliers()
        {
            var query = new GetAllSuppliers();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetSuppliersByTypeId/{typeId}")]
        public async Task<ActionResult<IEnumerable<NameResponse>>> GetSuppliersByTypeId(Guid typeId)
        {
            var query = new GetSuppliersByTypeRequest(typeId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }

        [HttpPost("AddSupplier")]
        public async Task<ActionResult<MessageResponse>> AddSupplier([FromBody] SupplierRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        [HttpPut("UpdateSupplier")]
        public async Task<ActionResult<MessageResponse>> UpdateSupplier([FromBody] UpdateSupplierRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        
        [HttpPut("DeleteSupplier")]
        public async Task<ActionResult<MessageResponse>> DeleteSupplier([FromBody] DeleteSupplierRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
