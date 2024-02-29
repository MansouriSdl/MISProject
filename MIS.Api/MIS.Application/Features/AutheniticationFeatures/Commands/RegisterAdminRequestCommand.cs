using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.AutheniticationFeatures.Commands
{
    public class RegisterAdminRequestCommand : IRequest<RegisterResponse>
    {
        public RegisterAdminRequestCommand(UserRegistration userRegistration)
        {
            UserRegistration = userRegistration;
        }

        public UserRegistration UserRegistration { get; set; }
    }
}
