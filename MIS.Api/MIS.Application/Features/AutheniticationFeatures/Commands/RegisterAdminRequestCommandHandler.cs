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
    public class RegisterAdminRequestCommandHandler : IRequestHandler<RegisterAdminRequestCommand, RegisterResponse>
    {
        readonly IAuthenticationAsync _authenticationAsync;

        public RegisterAdminRequestCommandHandler(IAuthenticationAsync authenticationAsync)
        {
            _authenticationAsync = authenticationAsync;
        }

        public async Task<RegisterResponse> Handle(RegisterAdminRequestCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationAsync.RegisterAdmin(request.UserRegistration);
        }
    }
}
