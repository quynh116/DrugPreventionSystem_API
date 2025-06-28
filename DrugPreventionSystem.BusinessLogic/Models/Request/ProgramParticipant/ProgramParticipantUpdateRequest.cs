using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant
{
    public class ProgramParticipantUpdateRequest
    {
        public Guid ProgramId { get; set; }
        public Guid UserId { get; set; }
        public bool Attended { get; set; }
        public bool FeedbackSubmitted { get; set; }
    }
}
