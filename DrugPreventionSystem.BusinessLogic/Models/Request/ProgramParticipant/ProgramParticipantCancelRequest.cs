using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant
{
    public class ProgramParticipantCancelRequest
    {
        public Guid ProgramId { get; set; }
        public Guid UserId { get; set; }
    }
} 