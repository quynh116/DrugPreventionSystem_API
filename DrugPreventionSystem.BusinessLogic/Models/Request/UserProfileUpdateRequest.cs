using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class UserProfileUpdateRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Occupation { get; set; }
        public string? EducationLevel { get; set; }
        public string? AgeGroup { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
