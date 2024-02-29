using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Constants;
using MIS.Domain.Entities;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class AuthenticationRepository : IAuthenticationAsync
    {
        private readonly MISDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(MISDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Login(UserLogin model)
        {
            var user = await _context.ApplicationUsers
                .AsSplitQuery()
                .Include(c => c.Employee.EmployeeType)
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == model.Username.ToLower() || u.Email.ToLower() == model.Username.ToLower());

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("id", user.Id.ToString()),
                        new Claim("email", user.Email),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("role", userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var expirationDate = DateTime.Now.AddMonths(1);
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: expirationDate,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new LoginResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenType = "Bearer",
                    Expiration = expirationDate,
                    UserId = user.EmployeeId.HasValue ? user.EmployeeId.Value : Guid.Empty,
                    FirstName = user.Employee.FirstName,
                    LastName = user.Employee.LastName,
                    IdentityCard = user.Employee.IdentityCard,
                    EmployeeType = user.Employee.EmployeeType.Name,
                    WorkId = user.Employee.WorkId,
                    UserName = user.UserName,
                    UserRoles = userRoles.ToList()
                };
            }
            return null;
        }
        [Authorize]
        public async Task<RegisterResponse> Register(UserRegistration model)
        {
            var res = 0;
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return null;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var appUserId = Guid.NewGuid().ToString();
                ApplicationUser applicationUser = new()
                {
                    Id = appUserId,
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.Phone,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };


                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                if (!result.Succeeded)
                    return null;

                string[] roles = { UserRoles.ADMIN, UserRoles.USER, UserRoles.SERVICE, UserRoles.PROCURMENT_DEPARTMENT, UserRoles.STOREKEEPER };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
                }

                foreach (var userRole in model.UserRoles)
                {
                    if (await _roleManager.RoleExistsAsync(userRole))
                    {
                        await _userManager.AddToRoleAsync(applicationUser, userRole);
                    }

                }

                Employee employee = await _context.Employees.FirstOrDefaultAsync(c => c.IdentityCard == model.IdentityCard);
                if (employee == null)
                {
                    employee = new()
                    {
                        IdentityCard = model.IdentityCard,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmployeeTypeId = model.EmployeeTypeId,
                        WorkId = model.WorkId,
                        ServiceId = model.ServiceId
                    };

                    _context.Employees.Add(employee);
                    res = await _context.SaveChangesAsync();

                }
                applicationUser.EmployeeId = employee.Id;
                _context.Entry(applicationUser).State = EntityState.Modified;
                res = await _context.SaveChangesAsync();

                if (res != 0)
                {
                    transaction.Commit();
                    return new RegisterResponse()
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        IdentityCard = model.IdentityCard,
                        WorkId = model.WorkId
                    };
                }
                return null;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
        [Authorize]

        public async Task<RegisterResponse> RegisterAdmin(UserRegistration model)
        {
            var res = 0;
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return null;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var appUserId = Guid.NewGuid().ToString();
                ApplicationUser applicationUser = new()
                {
                    Id = appUserId,
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.Phone,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };


                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                if (!result.Succeeded)
                    return null;

                string[] roles = { UserRoles.ADMIN, UserRoles.USER, UserRoles.SERVICE, UserRoles.PROCURMENT_DEPARTMENT, UserRoles.STOREKEEPER };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
                }


                if (await _roleManager.RoleExistsAsync(UserRoles.ADMIN))
                {
                    await _userManager.AddToRoleAsync(applicationUser, UserRoles.ADMIN);
                }


                Employee employee = await _context.Employees.FirstOrDefaultAsync(c => c.IdentityCard == model.IdentityCard);
                if (employee == null)
                {
                    employee = new()
                    {
                        IdentityCard = model.IdentityCard,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmployeeTypeId = model.EmployeeTypeId,
                        WorkId = model.WorkId
                    };

                    _context.Employees.Add(employee);
                    res = await _context.SaveChangesAsync();

                }
                applicationUser.EmployeeId = employee.Id;
                _context.Entry(applicationUser).State = EntityState.Modified;
                res = await _context.SaveChangesAsync();

                if (res != 0)
                {
                    transaction.Commit();
                    return new RegisterResponse()
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        IdentityCard = model.IdentityCard,
                        WorkId = model.WorkId
                    };
                }
                return null;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
    }
}
