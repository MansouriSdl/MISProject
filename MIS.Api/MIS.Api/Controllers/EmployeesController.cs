using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.EmployeeFeatures.Commands;
using MIS.Application.Features.EmployeeFeatures.Queries;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var query = new GetAllUsersRequest();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<MessageResponse>> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
        [HttpPut("DeleteUser")]
        public async Task<ActionResult<MessageResponse>> DeleteUser([FromBody] DeleteUserRequest request)
        {
            var result = await _mediator.Send(request);
            return result.StatusCode == 200 ? Ok() : BadRequest();
        }
    }
}
