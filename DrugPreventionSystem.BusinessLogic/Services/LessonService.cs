using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.BusinessLogic.Services.Quizzes;
using DrugPreventionSystem.DataAccess.Repository.Interfaces.IQuizzes;
using DrugPreventionSystem.DataAccess.Repository.Quizzes;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes;
using Microsoft.EntityFrameworkCore;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IUserModuleQuizResultRepository _userModuleQuizResultRepository;
        private readonly IUserQuizAnswerRepository _userQuizAnswerRepository;
        private readonly IUserLessonProgressRepository _userLessonProgressRepository;
        private readonly IUserCourseEnrollmentRepository _userCourseEnrollmentRepository;

        public LessonService(ILessonRepository lessonRepository, IUserLessonProgressRepository userLessonProgressRepository,
        IUserCourseEnrollmentRepository userCourseEnrollmentRepository, IQuizRepository quizRepository, IUserModuleQuizResultRepository userModuleQuizResultRepository, 
                             IUserQuizAnswerRepository userQuizAnswerRepository)
        {
            _userLessonProgressRepository = userLessonProgressRepository;
            _userCourseEnrollmentRepository = userCourseEnrollmentRepository;
            _lessonRepository = lessonRepository;
            _quizRepository = quizRepository;
            _userModuleQuizResultRepository = userModuleQuizResultRepository; 
            _userQuizAnswerRepository = userQuizAnswerRepository;
        }

        public async Task<Result<Lesson>> AddNewLessonAsync(LessonRequest lesson)
        {
            var newLesson = new Lesson()
            {
                WeekId = lesson.WeekId,
                Title = lesson.Title,
                DurationMinutes = lesson.DurationMinutes,
                Sequence = lesson.Sequence,
                HasQuiz = lesson.HasQuiz,
                HasPractice = lesson.HasPractice,
                CreatedAt = lesson.CreatedAt,
            };

            var added = await _lessonRepository.AddNewLesson(newLesson);
            return Result<Lesson>.Success(added, "Added successfully");
        }

        public async Task<Result<IEnumerable<LessonResponse>>> GetAllLessonsAsync()
        {
            var list = await _lessonRepository.GetAllLessonsAsync();
            var responseList = new List<LessonResponse>();
            foreach (var l in list)
            {
                responseList.Add(MapLessonToResponse(l));
            }
            return Result<IEnumerable<LessonResponse>>.Success(responseList);
        }

        public async Task<Result<LessonResponse>> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(id);
            if (lesson == null) return Result<LessonResponse>.NotFound($"Not found Lesson with id: {id}");
            return Result<LessonResponse>.Success(MapLessonToResponse(lesson));
        }

        public async Task<Result<bool>> DeleteLessonByIdAsync(Guid id)
        {
            await _lessonRepository.DeleteLessonByIdAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }

        public async Task<Result<Lesson>> UpdateLessonAsync(Guid id, Lesson lesson)
        {
            var existing = await _lessonRepository.GetLessonByIdAsync(id);
            if (existing == null) return Result<Lesson>.NotFound($"Not found Lesson with id: {id}");
            // Update fields
            existing.Title = lesson.Title;
            existing.WeekId = lesson.WeekId;
            existing.DurationMinutes = lesson.DurationMinutes;
            existing.Sequence = lesson.Sequence;
            existing.HasQuiz = lesson.HasQuiz;
            existing.HasPractice = lesson.HasPractice;
            await _lessonRepository.UpdateLessonAsync(existing);
            return Result<Lesson>.Success(existing, "Updated successfully");
        }

        private LessonResponse MapLessonToResponse(Lesson l)
        {
            return new LessonResponse
            {
                LessonId = l.LessonId,
                Title = l.Title,
                DurationMinutes = l.DurationMinutes,
                Sequence = l.Sequence,
                HasQuiz = l.HasQuiz,
                HasPractice = l.HasPractice,
                CreatedAt = l.CreatedAt,
                Resources = l.LessonResources?.Select(r => new LessonResourceResponse
                {
                    ResourceId = r.ResourceId,
                    LessonId = r.LessonId,
                    ResourceType = r.ResourceType,
                    ResourceUrl = r.ResourceUrl,
                    Description = r.Description
                }).ToList() ?? new List<LessonResourceResponse>()
            };
        }

        public async Task<Result<LessonDetailResponse>> GetLessonDetailsForUserAsync(Guid lessonId, Guid userId)
        {
            try
            {
                var lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);

                if (lesson == null)
                {
                    return Result<LessonDetailResponse>.NotFound($"Lesson with ID {lessonId} not found.");
                }

                if (lesson.CourseWeek?.Course == null)
                {
                    return Result<LessonDetailResponse>.Error("Course or CourseWeek information missing for this lesson.");
                }

                var course = lesson.CourseWeek.Course;

                


                
                var videoResource = lesson.LessonResources
                                          .FirstOrDefault(lr => lr.ResourceType.Equals("video", StringComparison.OrdinalIgnoreCase));

                
                int totalLessonsInCourse = course.CourseWeeks?.SelectMany(cw => cw.Lessons).Count() ?? 0;

                
                var userProgressesInCourse = await _userLessonProgressRepository.GetUserLessonProgressByUserIdAndCourseIdAsync(userId, course.CourseId);
                int completedLessonsByUser = userProgressesInCourse.Count(ulp => ulp.Passed);

                float courseProgressPercentage = totalLessonsInCourse > 0 ? (float)completedLessonsByUser / totalLessonsInCourse * 100 : 0;

                
                var currentUserLessonProgress = userProgressesInCourse.FirstOrDefault(ulp => ulp.LessonId == lessonId);
                bool isLessonCompleted = currentUserLessonProgress?.Passed ?? false;

                var lessonResourcesDto = lesson.LessonResources
                    .Select(lr => new LessonResourceDto
                    {
                        ResourceId = lr.ResourceId,
                        ResourceType = lr.ResourceType,
                        ResourceUrl = lr.ResourceUrl,
                        Description = lr.Description 
                    }).ToList();

                var response = new LessonDetailResponse
                {
                    LessonId = lesson.LessonId,
                    Title = lesson.Title,
                    Content = lesson.Content,
                    DurationMinutes = lesson.DurationMinutes,
                    VideoUrl = videoResource?.ResourceUrl,
                    Description = videoResource?.Description, 
                    CourseTitle = course.Title,
                    CourseId = course.CourseId,
                    CourseProgressPercentage = courseProgressPercentage,
                    IsCompleted = isLessonCompleted,
                    Resources = lessonResourcesDto,

                };

                return Result<LessonDetailResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<LessonDetailResponse>.Error($"Error getting lesson details: {ex.Message}");
            }
        }

        public async Task<Result<QuestionAndOptionResponse>> GetQuizQuestionsAndAnswersByLessonIdAsync(Guid lessonId)
        {
            var quiz = await _quizRepository.GetQuizByLessonIdAsync(lessonId);

            if (quiz == null)
            {
                return Result<QuestionAndOptionResponse>.NotFound($"Quiz not found for Lesson with ID {lessonId}.");
            }

            var quizResponse = new QuestionAndOptionResponse
            {
                QuizId = quiz.QuizId,
                LessonId = quiz.LessonId,
                Title = quiz.Title,
                Description = quiz.Description,
                TotalQuestions = quiz.QuizQuestions.Count, 
                PassingScore = quiz.PassingScore,
                Questions = quiz.QuizQuestions.Select(qq => new QuizQuestionResponse1
                {
                    QuestionId = qq.QuestionId,
                    QuestionText = qq.QuestionText,
                    QuestionType = qq.QuestionType,
                    Sequence = qq.Sequence,
                    Options = qq.QuizOptions.Select(qo => new QuizOptionResponse1
                    {
                        OptionId = qo.OptionId,
                        OptionText = qo.OptionText,
                        IsCorrect = qo.IsCorrect // Bao g?m c? IsCorrect ?? tr? l?i
                    }).ToList()
                }).ToList()
            };

            return Result<QuestionAndOptionResponse>.Success(quizResponse);
        }

        public async Task<Result<QuizResultResponse>> SubmitQuizAttemptAsync(SubmitQuizRequest request)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
            if (quiz == null)
            {
                return Result<QuizResultResponse>.NotFound($"Quiz with ID {request.QuizId} not found.");
            }

            int correctCount = 0;
            var userAnswersToSave = new List<UserQuizAnswer>();
            var questionsWithResults = new List<QuizResultQuestionResponse>();

            float scorePerQuestion = quiz.QuizQuestions.Any() ? 100.0f / quiz.QuizQuestions.Count : 0;

            foreach (var userAnswerRequest in request.Answers)
            {
                var question = quiz.QuizQuestions.FirstOrDefault(q => q.QuestionId == userAnswerRequest.QuestionId);
                if (question == null) continue;

                bool isCorrect = false;
                string? userSelectedOptionText = null;

                var userSelectedOption = question.QuizOptions.FirstOrDefault(o => o.OptionId == userAnswerRequest.SelectedOptionId);
                if (userSelectedOption != null)
                {
                    userSelectedOptionText = userSelectedOption.OptionText;
                }

                var correctOption = question.QuizOptions.FirstOrDefault(o => o.IsCorrect);
                if (correctOption != null && userAnswerRequest.SelectedOptionId.HasValue && userAnswerRequest.SelectedOptionId.Value == correctOption.OptionId)
                {
                    isCorrect = true;
                    correctCount++;
                }

                
                userAnswersToSave.Add(new UserQuizAnswer
                {
                    UserId = request.UserId,
                    QuestionId = userAnswerRequest.QuestionId,
                    SelectedOptionId = userAnswerRequest.SelectedOptionId,
                    AnswerText = null,
                    AnsweredAt = DateTime.Now 
                });

                questionsWithResults.Add(new QuizResultQuestionResponse
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    QuestionType = question.QuestionType,
                    Sequence = question.Sequence,
                    UserSelectedOptionId = userAnswerRequest.SelectedOptionId,
                    UserSelectedOptionText = userSelectedOptionText,
                    IsUserAnswerCorrect = isCorrect
                });
            }

            float totalScore = correctCount * scorePerQuestion;
            string status = (totalScore >= (quiz.PassingScore ?? 0)) ? "passed" : "failed";

            
            var existingQuizResult = await _userModuleQuizResultRepository.GetLatestUserModuleQuizResultForLessonAsync(request.UserId, quiz.LessonId);
            UserModuleQuizResult userQuizResult;

            if (existingQuizResult == null)
            {
                // Tạo mới UserModuleQuizResult
                userQuizResult = new UserModuleQuizResult
                {
                    ResultId = Guid.NewGuid(),
                    UserId = request.UserId,
                    LessonId = quiz.LessonId,
                    TotalScore = totalScore,
                    CorrectCount = correctCount,
                    TotalQuestions = quiz.QuizQuestions.Count,
                    Status = status,
                    TakenAt = DateTime.Now
                };
                await _userModuleQuizResultRepository.AddUserModuleQuizResultAsync(userQuizResult);
            }
            else
            {
                
                userQuizResult = existingQuizResult;
                userQuizResult.TotalScore = totalScore;
                userQuizResult.CorrectCount = correctCount;
                userQuizResult.TotalQuestions = quiz.QuizQuestions.Count;
                userQuizResult.Status = status;
                userQuizResult.TakenAt = DateTime.Now; 
                await _userModuleQuizResultRepository.UpdateUserModuleQuizResultAsync(userQuizResult);

                
                var oldUserAnswers = await _userQuizAnswerRepository.GetUserQuizAnswersByUserIdAndQuizIdAsync(request.UserId, quiz.QuizId); 
                foreach (var oldAnswer in oldUserAnswers)
                {
                    await _userQuizAnswerRepository.DeleteUserQuizAnswerAsync(oldAnswer.UserQuizAnswerId);
                }
            }

           
            foreach (var answer in userAnswersToSave)
            {
                
                answer.AnsweredAt = userQuizResult.TakenAt;
                await _userQuizAnswerRepository.AddUserQuizAnswerAsync(answer);
            }

            
            var userLessonProgress = await _userLessonProgressRepository.GetUserLessonProgressByUserIdAndLessonIdAsync(request.UserId, quiz.LessonId);

            if (userLessonProgress == null)
            {
                userLessonProgress = new UserLessonProgress
                {
                    ProgressId = Guid.NewGuid(),
                    UserId = request.UserId,
                    LessonId = quiz.LessonId,
                    CompletedAt = DateTime.Now,
                    QuizScore = totalScore,
                    Passed = false,
                    UpdatedAt = DateTime.Now
                };
                await _userLessonProgressRepository.AddUserLessonProgressAsync(userLessonProgress);
            }
            else
            {
                userLessonProgress.CompletedAt = DateTime.Now;
                userLessonProgress.QuizScore = totalScore;
                userLessonProgress.UpdatedAt = DateTime.Now;
                await _userLessonProgressRepository.UpdateUserLessonProgressAsync(userLessonProgress);
            }

            var resultResponse = new QuizResultResponse
            {
                ResultId = userQuizResult.ResultId,
                QuizId = quiz.QuizId,
                LessonId = quiz.LessonId,
                QuizTitle = quiz.Title,
                TotalScore = totalScore,
                CorrectCount = correctCount,
                TotalQuestions = userQuizResult.TotalQuestions,
                Status = status,
                TakenAt = userQuizResult.TakenAt,
                PassedQuizThreshold = status == "passed",
                QuestionsAttempted = questionsWithResults
            };

            return Result<QuizResultResponse>.Success(resultResponse, "Quiz submitted successfully.");
        }

        public async Task<Result<QuizResultResponse>> GetUserQuizResultForLessonAsync(Guid userId, Guid lessonId)
        {
            var userQuizResult = await _userModuleQuizResultRepository.GetLatestUserModuleQuizResultForLessonAsync(userId, lessonId);

            if (userQuizResult == null)
            {
                return Result<QuizResultResponse>.NotFound($"No quiz result found for User {userId} on Lesson {lessonId}.");
            }

            var quiz = await _quizRepository.GetQuizByLessonIdAsync(userQuizResult.LessonId);
            if (quiz == null)
            {
                return Result<QuizResultResponse>.Error("Associated quiz not found for this result.");
            }

            
            var userAnswers = await _userQuizAnswerRepository.GetUserQuizAnswersForQuizAttemptAsync(userId, quiz.QuizId, userQuizResult.TakenAt);

            var questionsWithResults = new List<QuizResultQuestionResponse>();

            foreach (var question in quiz.QuizQuestions.OrderBy(q => q.Sequence))
            {
                var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == question.QuestionId);
                bool isCorrect = false;
                string? userSelectedOptionText = null;

                var userSelectedOption = question.QuizOptions.FirstOrDefault(o => o.OptionId == userAnswer?.SelectedOptionId);
                if (userSelectedOption != null)
                {
                    userSelectedOptionText = userSelectedOption.OptionText;
                }

                var correctOption = question.QuizOptions.FirstOrDefault(o => o.IsCorrect);
                if (correctOption != null && userAnswer?.SelectedOptionId.HasValue == true && userAnswer.SelectedOptionId.Value == correctOption.OptionId)
                {
                    isCorrect = true;
                }

                questionsWithResults.Add(new QuizResultQuestionResponse
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    QuestionType = question.QuestionType,
                    Sequence = question.Sequence,
                    UserSelectedOptionId = userAnswer?.SelectedOptionId,
                    UserSelectedOptionText = userSelectedOptionText,
                    IsUserAnswerCorrect = isCorrect
                });
            }

            var resultResponse = new QuizResultResponse
            {
                ResultId = userQuizResult.ResultId,
                QuizId = quiz.QuizId,
                LessonId = quiz.LessonId,
                QuizTitle = quiz.Title,
                TotalScore = userQuizResult.TotalScore,
                CorrectCount = userQuizResult.CorrectCount,
                TotalQuestions = userQuizResult.TotalQuestions,
                Status = userQuizResult.Status,
                TakenAt = userQuizResult.TakenAt,
                PassedQuizThreshold = userQuizResult.Status == "passed",
                QuestionsAttempted = questionsWithResults
            };

            return Result<QuizResultResponse>.Success(resultResponse);
        }

        public async Task<Result<bool>> CompleteLessonAsync(CompleteLessonRequest request)
        {
            var userLessonProgress = await _userLessonProgressRepository.GetUserLessonProgressByUserIdAndLessonIdAsync(request.UserId, request.LessonId);

            if (userLessonProgress == null)
            {
                return Result<bool>.NotFound("Bạn cần làm bài học này trước khi đánh dấu hoàn thành.");
            }

            // 2. Kiểm tra kết quả quiz gần nhất
            var quizForLesson = await _quizRepository.GetQuizByLessonIdAsync(request.LessonId);
            if (quizForLesson == null)
            {
                // Nếu bài học không có quiz, có thể cho phép hoàn thành luôn
                // hoặc trả về lỗi nếu mọi bài học phải có quiz để hoàn thành.
                // Tùy thuộc vào quy tắc nghiệp vụ của bạn.
                // Ví dụ: return Result<bool>.Error("Bài học này không có quiz để đánh giá hoàn thành.");
                userLessonProgress.Passed = true; // Nếu không có quiz, tự động hoàn thành
                userLessonProgress.UpdatedAt = DateTime.Now;
                await _userLessonProgressRepository.UpdateUserLessonProgressAsync(userLessonProgress);
                return Result<bool>.Success(true, "Bài học đã được đánh dấu hoàn thành (không có quiz).");
            }

            var latestQuizResult = await _userModuleQuizResultRepository.GetLatestUserModuleQuizResultForLessonAsync(request.UserId, request.LessonId);

            if (latestQuizResult == null || latestQuizResult.Status != "passed")
            {
                return Result<bool>.Error("Bạn cần hoàn thành và đỗ bài kiểm tra của bài học này để đánh dấu hoàn thành.");
            }

            // 3. Nếu đủ điều kiện, cập nhật UserLessonProgress.Passed
            if (!userLessonProgress.Passed) // Chỉ cập nhật nếu chưa được đánh dấu hoàn thành
            {
                userLessonProgress.Passed = true;
                userLessonProgress.UpdatedAt = DateTime.Now;
                await _userLessonProgressRepository.UpdateUserLessonProgressAsync(userLessonProgress);
            }

            return Result<bool>.Success(true, "Bài học đã được đánh dấu hoàn thành.");
        }

        public async Task<Result<QuizStatusResponse>> GetQuizInitialStateForUserAsync(Guid lessonId, Guid userId, bool forceAttempt = false)
        {
            var quiz = await _quizRepository.GetQuizByLessonIdAsync(lessonId);
            if (quiz == null)
            {
                return Result<QuizStatusResponse>.NotFound($"Quiz not found for Lesson with ID {lessonId}.");
            }

            var quizAttemptData = new QuestionAndOptionResponse
            {
                QuizId = quiz.QuizId,
                LessonId = quiz.LessonId,
                Title = quiz.Title,
                Description = quiz.Description,
                TotalQuestions = quiz.QuizQuestions.Count,
                PassingScore = quiz.PassingScore,
                Questions = quiz.QuizQuestions.Select(qq => new QuizQuestionResponse1
                {
                    QuestionId = qq.QuestionId,
                    QuestionText = qq.QuestionText,
                    QuestionType = qq.QuestionType,
                    Sequence = qq.Sequence,
                    Options = qq.QuizOptions.Select(qo => new QuizOptionResponse1
                    {
                        OptionId = qo.OptionId,
                        OptionText = qo.OptionText,
                        // Luôn bao gồm IsCorrect ở Backend, Frontend sẽ không sử dụng để tránh gian lận
                        IsCorrect = qo.IsCorrect
                    }).ToList()
                }).ToList()
            };

            // 1. Kiểm tra xem người dùng đã từng làm quiz này chưa
            var latestUserQuizResult = await _userModuleQuizResultRepository.GetLatestUserModuleQuizResultForLessonAsync(userId, lessonId);

            // Logic chính: Nếu forceAttempt là true, hoặc người dùng chưa từng làm bài
            if (forceAttempt || latestUserQuizResult == null)
            {
                // Luôn trả về dữ liệu câu hỏi để họ bắt đầu làm bài mới
                return Result<QuizStatusResponse>.Success(new QuizStatusResponse
                {
                    QuizId = quiz.QuizId,
                    LessonId = lessonId,
                    QuizTitle = quiz.Title,
                    DisplayMode = QuizDisplayMode.AttemptQuiz, // Frontend hiển thị màn hình làm bài
                    LatestQuizResultData = null,
                    QuizAttemptData = quizAttemptData          // Gửi dữ liệu câu hỏi
                });
            }
            else
            {
                
                var resultResponse = await GetUserQuizResultForLessonAsync(userId, lessonId);

                if (resultResponse.ResultStatus == ResultStatus.Success && resultResponse.Data != null)
                {
                    return Result<QuizStatusResponse>.Success(new QuizStatusResponse
                    {
                        QuizId = quiz.QuizId,
                        LessonId = lessonId,
                        QuizTitle = quiz.Title,
                        DisplayMode = QuizDisplayMode.ViewResult,        
                        LatestQuizResultData = resultResponse.Data, 
                        QuizAttemptData = null                           
                    });
                }
                else
                {
                   
                    return Result<QuizStatusResponse>.Error("Không thể lấy kết quả quiz gần nhất mặc dù đã có bản ghi.");
                }
            }
        }
        }

} 