using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz
{
    public class QuizFullEditDto
    {
        public Guid QuizId { get; set; }


        public Guid LessonId { get; set; } // Liên kết với Lesson

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public float? PassingScore { get; set; }

        // Navigation property ảo cho các câu hỏi
        public List<QuizQuestionFullEditDto> Questions { get; set; } = new List<QuizQuestionFullEditDto>();
    }
    public class QuizQuestionFullEditDto
    {
        public Guid QuestionId { get; set; }

        // Để biết câu hỏi thuộc Quiz nào. Khi gửi lên, có thể không cần thiết nếu QuizId đã có ở QuizFullEditDto
        // nhưng hữu ích để backend kiểm tra tính hợp lệ.
        public Guid QuizId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public string? QuestionType { get; set; } // single_choice, multi_choice, text

        public int Sequence { get; set; }

        public List<QuizOptionDto> Options { get; set; } = new List<QuizOptionDto>();
    }
    public class QuizOptionDto
    {
        public Guid OptionId { get; set; }

        // Để biết tùy chọn thuộc câu hỏi nào.
        public Guid QuestionId { get; set; }

        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;
    }
}