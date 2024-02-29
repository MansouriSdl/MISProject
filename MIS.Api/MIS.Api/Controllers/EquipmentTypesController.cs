using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.EquipmentTypeFeatures.Commands;
using MIS.Application.Features.EquipmentTypeFeatures.Queries;
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
    public class EquipmentTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EquipmentTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllEquipmentTypes")]
        public async Task<ActionResult<IEnumerable<EquipmentTypeResponse>>> GetAllEquipmentTypes()
        {
            var query = new GetAllEquipmentTypes();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("AddEquipmentType")]
        public async Task<ActionResult<MessageResponse>> AddEquipmentType([FromBody] EquipmentTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }


        [HttpPut("UpdateEquipmentType")]
        public async Task<ActionResult<MessageResponse>> UpdateEquipmentType([FromBody] UpdateEquipmentTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }

        [HttpPut("DeleteEquipmentType")]
        public async Task<ActionResult<MessageResponse>> DeleteEquipmentType([FromBody] DeleteEquipmentTypeRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
