﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Survey
{
    public class SurveyUpdateRequest
    {
        
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
