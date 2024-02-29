using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.BureauEquipmentAssignmentFeatures.Queries;
using MIS.Application.Features.BureauStockInventoryFeatures.Commands;
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
    public class BureauStockInventoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BureauStockInventoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("PostBureauStockInventory")]
        public async Task<ActionResult<BureauStockInventoryResponse>> PostBureauStockInventory([FromBody] PostBureauStockInventory request)
        {
            var result =  await _mediator.Send(request);
            return  result.Code == 2 ? Ok(result)  : BadRequest(result);
        }

        [HttpPost("GetBureauStockInventoryHistory")]
        public async Task<ActionResult<InventoryHistoryResponse>> GetBureauStockInventoryHistory([FromBody] BureauStockInventoryFilterRequest request)
        {
            var query = new GetBureauStockInventoryHistory(request);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }
        [HttpPost("GetInventoriedStockCount")]
        public async Task<ActionResult<List<InventoriedStockCountResponse>>> GetInventoriedStockCount([FromBody] BureauStockInventoryFilterRequest request)
        {
            var query = new GetInventoriedStockCount(request);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut("UpdateInventorysAvailability")]
        public async Task<ActionResult<int>> UpdateInventorysAvailability(List<UpdateInventoryAvailabilityRequest> request)
        {
            UpdateInventoryAvailability data = new UpdateInventoryAvailability(request);
            var result = await _mediator.Send(data);
            return (int) result != 0 ? Ok(result) : BadRequest(result);
        }

        [HttpPost("PostInventoryForNewEquipments")]
        public async Task<ActionResult<BureauStockInventoryResponse>> PostInventoryForNewEquipments([FromBody] PostInventoryForNewEquipmentsRequest request)
        {
            var result = await _mediator.Send(request);
            return result.Code == 2 ? Ok(result) : BadRequest(result);
        }
    }
}
