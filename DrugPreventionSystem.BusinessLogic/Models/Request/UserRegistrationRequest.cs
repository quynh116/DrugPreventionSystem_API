using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class UserRegistrationRequest
    {
        
        public string Username { get; set; } = string.Empty;

        
        public string Email { get; set; } = string.Empty;

        
        public string Password { get; set; } = string.Empty;
        
    }
}
