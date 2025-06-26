using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Lesson
{
    public class CompleteLessonRequest
    {
        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
    }
}
