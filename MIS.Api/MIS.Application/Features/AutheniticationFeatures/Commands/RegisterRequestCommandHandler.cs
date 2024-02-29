using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.AutheniticationFeatures.Commands
{
    public class RegisterRequestCommandHandler : IRequestHandler<RegisterRequestCommand, RegisterResponse>
    {
        readonly IAuthenticationAsync _authenticationAsync;

        public RegisterRequestCommandHandler(IAuthenticationAsync authenticationAsync)
        {
            _authenticationAsync = authenticationAsync;
        }

        public async Task<RegisterResponse> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationAsync.Register(request.UserRegistration);
        }
    }
}
