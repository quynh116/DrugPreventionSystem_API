using DrugPreventionSystem.BusinessLogic.Services.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz
{
    public class QuizStatusResponse
    {
        public Guid QuizId { get; set; }
        public Guid LessonId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;

        // Enum để Frontend biết nên hiển thị gì
        public QuizDisplayMode DisplayMode { get; set; }

        // Dữ liệu quiz để làm bài (nếu DisplayMode là AttemptQuiz)
        public QuestionAndOptionResponse? QuizAttemptData { get; set; }

        // Dữ liệu kết quả quiz gần nhất (nếu DisplayMode là ViewResult)
        public QuizResultResponse? LatestQuizResultData { get; set; }
    }

    public enum QuizDisplayMode
    {
        AttemptQuiz = 0, // Hiển thị màn hình làm bài
        ViewResult = 1   // Hiển thị màn hình kết quả làm bài gần nhất
    }
}

