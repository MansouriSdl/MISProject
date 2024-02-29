using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.QrCodeFeatures.Commands;
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
    public class QrCodesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QrCodesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddListOfQrCodes")]
        public async Task<ActionResult<List<QrCodeResponse>>> AddListOfQrCodes([FromBody] AddListOfQrCodes request)
        {
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}
