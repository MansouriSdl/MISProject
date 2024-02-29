using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class UserRegistration
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IdentityCard { get; set; }
        
        public string WorkId { get; set; }
        [Required]
        public Guid EmployeeTypeId { get; set; }
        [Required]
        public string Phone { get; set; }
        public string[] UserRoles { get; set; }

        public Guid? ServiceId { get; set; }
    }
}
