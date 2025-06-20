using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Course
{
    public class CourseWeekRequest
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int WeekNumber { get; set; }
    }
}
