using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IAuthenticationAsync
    {
        Task<LoginResponse> Login(UserLogin model);
        Task<RegisterResponse> Register(UserRegistration model);
        Task<RegisterResponse> RegisterAdmin(UserRegistration model);
    }
}
