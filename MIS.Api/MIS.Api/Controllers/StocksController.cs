using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.StockFeatures.Commands;
using MIS.Application.Features.StockFeatures.Queries;
using MIS.Application.Models.Requests;
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

    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllStocks")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllStocks()
        {
            var query = new GetAllStocks();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetStocksByEquipmentId/{equipmentId}")]
        public async Task<ActionResult<IEnumerable<NameResponse>>> GetStocksByEquipmentId(Guid equipmentId)
        {
            var query = new GetStocksByEquipmentIdRequest(equipmentId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("GetStocksList")]
        public async Task<ActionResult<List<StockResponse>>> GetStocksList(FilterRequest request)
        {
            var query = new GetStocksListRequest(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("PostStock")]
        public async Task<ActionResult<MessageResponse>> PostStock([FromBody] AddStockWithoutOrderRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        [HttpPost("PostStockList")]
        public async Task<ActionResult<MessageResponse>> PostStockList([FromBody] AddStockListRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }


        [HttpPost("TopFiveConsummableEquipments")]
        public async Task<ActionResult<List<NameValueResponse>>> TopFiveConsummableEquipments(FilterRequest request)
        {
            var query = new TopFiveConsummableEquipmentsRequest(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("GetAlerts")]
        public async Task<ActionResult<AlertResponse>> GetAlerts()
        {
            var query = new GetAlertsRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
