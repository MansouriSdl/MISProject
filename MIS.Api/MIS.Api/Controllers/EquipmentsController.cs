using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.EquipmentFeatures.Commands;
using MIS.Application.Features.EquipmentFeatures.Queries;
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

    public class EquipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EquipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllEquipments")]
        public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetAllEquipments()
        {
            var query = new GetAllEquipments();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost("AddEquipment")]
        public async Task<ActionResult<MessageResponse>> AddEquipment([FromBody] EquipmentRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        [HttpPut("UpdateEquipment")]
        public async Task<ActionResult<MessageResponse>> UpdateEquipment([FromBody] UpdateEquipmentRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        
        [HttpPut("DeleteEquipment")]
        public async Task<ActionResult<MessageResponse>> DeleteEquipment([FromBody] DeleteEquipmentRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
