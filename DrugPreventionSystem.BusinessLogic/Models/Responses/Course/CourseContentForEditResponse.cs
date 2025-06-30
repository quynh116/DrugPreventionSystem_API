using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class CourseContentForEditResponse
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public Guid InstructorId { get; set; }
        public string InstructorName { get; set; }
        // Thêm các thuộc tính khác của Course
        // ...

        public List<CourseWeekEditDto2> CourseWeeks { get; set; } = new List<CourseWeekEditDto2>();
    }

    public class CourseWeekEditDto2
    {
        public Guid WeekId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public int WeekNumber { get; set; }

        public List<LessonEditDto2> Lessons { get; set; } = new List<LessonEditDto2>();
    }

    public class LessonEditDto2
    {
        public Guid LessonId { get; set; }
        public Guid WeekId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public int? DurationMinutes { get; set; }
        public int Sequence { get; set; }
        public bool HasQuiz { get; set; } // Vẫn giữ để biết bài học này có quiz hay không
        public bool HasPractice { get; set; }

        public List<LessonResourceDto2> Resources { get; set; } = new List<LessonResourceDto2>();
        // KHÔNG CÓ QuizFullEditDto ở đây nữa
    }

    public class LessonResourceDto2 // Giữ nguyên
    {
        public Guid ResourceId { get; set; }
        public Guid LessonId { get; set; }
        public string ResourceType { get; set; }
        public string ResourceUrl { get; set; }
        public string? Description { get; set; }
    }
    }