using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram
{
    public class CommunityProgramCreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? TargetAudience { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public int? MaxParticipants { get; set; }
    }
}
