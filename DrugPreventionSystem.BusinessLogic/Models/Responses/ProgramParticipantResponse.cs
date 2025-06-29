using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class ProgramParticipantResponse
    {
        public Guid ParticipantId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        public bool Attended { get; set; }
        public bool FeedbackSubmitted { get; set; }

    }
}
