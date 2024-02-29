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
    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, LoginResponse>
    {
        readonly IAuthenticationAsync _authenticationAsync;

        public LoginRequestCommandHandler(IAuthenticationAsync authenticationAsync)
        {
            _authenticationAsync = authenticationAsync;
        }

        public async Task<LoginResponse> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationAsync.Login(request.UserLogin);
        }
    }
}
