using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson
{
    public class LessonResourceResponse
    {
        public Guid ResourceId { get; set; } = Guid.NewGuid();

        public Guid LessonId { get; set; }

        public string ResourceType { get; set; } = string.Empty;

        public string ResourceUrl { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}
