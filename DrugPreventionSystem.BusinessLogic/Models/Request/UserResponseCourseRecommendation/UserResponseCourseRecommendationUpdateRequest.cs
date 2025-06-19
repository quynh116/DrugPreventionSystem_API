using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.UserResponseCourseRecommendation
{
    public class UserResponseCourseRecommendationUpdateRequest
    {
        public Guid ResponseId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime RecommendedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
