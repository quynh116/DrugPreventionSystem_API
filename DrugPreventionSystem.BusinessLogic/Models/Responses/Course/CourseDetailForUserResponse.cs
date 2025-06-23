using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class CourseDetailForUserResponse
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? InstructorName { get; set; } 
        public int? DurationWeeks { get; set; }


        public float CourseProgressPercentage { get; set; } 
        public int TotalLessonsInCourse { get; set; } 
        public int CompletedLessonsByUser { get; set; } 

        // Trạng thái đăng ký
        public bool IsEnrolled { get; set; } = false;
    }

}
