using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.AutheniticationFeatures.Commands;
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
    public class AuthenticationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserLogin model)
        {
            var query = new LoginRequestCommand(model);
            var Result = await _mediator.Send(query);
            return Result != null ? Ok(Result) : BadRequest();
        }
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] UserRegistration model)
        {
            var query = new RegisterRequestCommand(model);
            var Result = await _mediator.Send(query);
            return Result != null ? Ok(Result) : BadRequest();
        }
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult<RegisterResponse>> RegisterAdmin([FromBody] UserRegistration model)
        {
            var query = new RegisterAdminRequestCommand(model);
            var Result = await _mediator.Send(query);
            return Result != null ? Ok(Result) : BadRequest();
        }
    }
}
