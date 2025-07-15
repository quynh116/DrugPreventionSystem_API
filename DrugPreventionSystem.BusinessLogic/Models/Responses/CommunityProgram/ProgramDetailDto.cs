using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram
{
    public class ProgramDetailDto
    {
        public Guid ProgramId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? TargetAudience { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MaxParticipants { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? SurveyId { get; set; } 

        public int CurrentParticipantsCount { get; set; }

        public List<ParticipantStatusDto> Participants { get; set; } = new List<ParticipantStatusDto>();
    }

    public class ParticipantStatusDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; } 
        public bool HasSubmittedSurvey { get; set; } 
    }
}
