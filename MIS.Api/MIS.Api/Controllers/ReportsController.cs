using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly IMediator _mediator;
        [HttpPost("AddListOfQrCodes")]
        public async Task<ActionResult<List<>>> AddListOfQrCodes([FromBody] AddLitOfQrCodes request)
        {
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest(result);

        }
    }
