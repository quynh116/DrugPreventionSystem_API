﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserLessonProgress
{
    public class UserLessonProgressResponse
    {
        public Guid ProgressId { get; set; }
        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
        public DateTime? CompletedAt { get; set; }
        public float? QuizScore { get; set; }
        public bool Passed { get; set; } = false;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
