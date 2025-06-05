﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class UserUpdateRequest
    {
        public string Username { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public bool? EmailVerified { get; set; }
        public int? RoleId { get; set; }
    }
}
