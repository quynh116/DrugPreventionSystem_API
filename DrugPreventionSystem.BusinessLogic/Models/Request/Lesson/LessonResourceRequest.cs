using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Lesson
{
    public class LessonResourceRequest
    {
        public Guid LessonId { get; set; }

        public string ResourceType { get; set; } = string.Empty; 

        public string ResourceUrl { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
