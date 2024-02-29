using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.OrderFeatures.Commands;
using MIS.Application.Features.OrderFeatures.Queries;
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
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAllOrders")]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders([FromBody] FilterRequest request)
        {
            var query = new GetAllOrders(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("GetPendingOrders")]
        public async Task<ActionResult<List<OrderResponse>>> GetPendingOrders(FilterRequest request)
        {
            var query = new GetPendingOrdersRequest(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("PostOrder")]
        public async Task<ActionResult<MessageResponse>> PostDivisionEquipmentRequest([FromBody] AddOrderRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        [HttpPost("PostOrdersList")]
        public async Task<ActionResult<MessageResponse>> PostOrdersList([FromBody] AddOrdersListRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
