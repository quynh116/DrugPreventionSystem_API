﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant
{
    public class ProgramParticipantCreateRequest
    {
        public Guid ProgramId { get; set; }
        public Guid UserId { get; set; }
        

    }
}
