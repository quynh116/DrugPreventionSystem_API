using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Request.Survey;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyResponse;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyResponse;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserSurveyResponseService : IUserSurveyResponseService
    {
        private readonly IUserSurveyResponseRepository _userSurveyResponseRepository;
        private readonly IUserSurveyAnswerRepository _userSurveyAnswerRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyOptionRepository _surveyOptionRepository;
        private readonly ISurveyCourseRecommendationRepository _surveyCourseRecommendationRepository;
        private readonly IUserResponseCourseRecommendationRepository _userResponseCourseRecommendationRepository;

        public UserSurveyResponseService(IUserSurveyResponseRepository userSurveyResponseRepository, IUserSurveyAnswerRepository userSurveyAnswerRepository,
            ISurveyRepository surveyRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            ISurveyOptionRepository surveyOptionRepository,
            ISurveyCourseRecommendationRepository surveyCourseRecommendationRepository,
            IUserResponseCourseRecommendationRepository userResponseCourseRecommendationRepository)
        {
            _userSurveyResponseRepository = userSurveyResponseRepository;
            _userSurveyAnswerRepository = userSurveyAnswerRepository;
            _surveyRepository = surveyRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyOptionRepository = surveyOptionRepository;
            _surveyCourseRecommendationRepository = surveyCourseRecommendationRepository;
            _userResponseCourseRecommendationRepository = userResponseCourseRecommendationRepository;
        }

        public UserSurveyResponseResponse MapToResponse(UserSurveyResponse user)
        {
            return new UserSurveyResponseResponse
            {
                ResponseId = user.ResponseId,
                UserId = user.UserId,
                SurveyId = user.SurveyId,
                TakenAt = user.TakenAt,
                RiskLevel = user.RiskLevel,
                RecommendedAction = user.RecommendedAction,
            };
        }

        public async Task<Result<IEnumerable<UserSurveyResponseResponse>>> GetAllAsync()
        {
            try
            {
                var result = await _userSurveyResponseRepository.GetAllAsync();
                var response = result.Select(r => MapToResponse(r)).ToList();
                return Result<IEnumerable<UserSurveyResponseResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserSurveyResponseResponse>>.Error($"Error retrieving users: {ex.Message}");
            }

        }
        public async Task<Result<UserSurveyResponseResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _userSurveyResponseRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return Result<UserSurveyResponseResponse>.NotFound($"UserSurveyResponse with ID {id} not found.");
                }
                return Result<UserSurveyResponseResponse>.Success(MapToResponse(result));
            }
            catch (Exception ex)
            {
                return Result<UserSurveyResponseResponse>.Error($"Error retrieving users: {ex.Message}");
            }
        }
        public async Task<Result<UserSurveyResponseResponse>> CreateAsync(UserSurveyResponseCreateRequest request)
        {
            var newResponse = new UserSurveyResponse()
            {
                ResponseId = Guid.NewGuid(),
                UserId = request.UserId,
                SurveyId = request.SurveyId,
                RiskLevel = request.RiskLevel,
                RecommendedAction = request.RecommendedAction,
            };
            var createResonpse = await _userSurveyResponseRepository.CreateAsync(newResponse);
            return Result<UserSurveyResponseResponse>.Success(MapToResponse(createResonpse));
        }

        public async Task<Result<UserSurveyResponseResponse>> UpdateAsync(UserSurveyResponseUpdateRequest request, Guid id)
        {
            try
            {
                var response = await _userSurveyResponseRepository.GetByIdAsync(id);
                if (response == null)
                {
                    return Result<UserSurveyResponseResponse>.NotFound($"Survey response with ID {id} not found.");
                }
                response.TakenAt = request.TakenAt;
                response.RiskLevel = request.RiskLevel;
                response.RecommendedAction = request.RecommendedAction;
                await _userSurveyResponseRepository.UpdateAsync(response);
                return Result<UserSurveyResponseResponse>.Success(MapToResponse(response));

            }
            catch (Exception ex)
            {
                {
                    return Result<UserSurveyResponseResponse>.Error($"Error updating survey response: {ex.Message}");
                }

            }
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _userSurveyResponseRepository.GetByIdAsync(id);
                if (response == null)
                {
                    return Result<bool>.NotFound($"Survey response with ID {id} not found.");
                }
                await _userSurveyResponseRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Survey response deleted successfully.");
            }
            catch (Exception ex)
            {
                {
                    return Result<bool>.Error($"Error updating survey response: {ex.Message}");
                }

            }
        }

        // 1. Phương thức để bắt đầu một phiên khảo sát mới
        public async Task<Result<StartSurveyResponseDto>> StartNewSurveySession(StartSurveyRequestDto request)
        {
            try
            {
                var survey = await _surveyRepository.GetSurveyByIdAsync(request.SurveyId);
                if (survey == null)
                {
                    return Result<StartSurveyResponseDto>.NotFound("Khảo sát không tồn tại.");
                }
                // Optional: Check if user exists (requires IUserRepository)
                // var user = await _userRepository.GetUserByIdAsync(request.UserId);
                // if (user == null) return Result<StartSurveyResponseDto>.NotFound("Người dùng không tồn tại.");

                var newResponse = new UserSurveyResponse
                {
                    UserId = request.UserId,
                    SurveyId = request.SurveyId,
                    TakenAt = DateTime.Now,
                };

                newResponse = await _userSurveyResponseRepository.CreateAsync(newResponse);

                return Result<StartSurveyResponseDto>.Success(new StartSurveyResponseDto
                {
                    ResponseId = newResponse.ResponseId,
                    UserId = newResponse.UserId,
                    SurveyId = newResponse.SurveyId,
                    TakenAt = newResponse.TakenAt,
                    Message = "Phiên khảo sát mới đã được bắt đầu."
                }, "Phiên khảo sát mới đã được bắt đầu."); // Pass message explicitly for your Result.Success(data, message)
            }
            catch (Exception ex)
            {
                return Result<StartSurveyResponseDto>.Error($"Lỗi khi bắt đầu phiên khảo sát mới: {ex.Message}");
            }
        }

        // 2. Phương thức để lưu từng câu trả lời khi người dùng nhấn "Tiếp theo"
        public async Task<Result<SaveSingleAnswerResponseDto>> SaveSingleAnswer(SaveSingleAnswerRequestDto request)
        {
            try
            {
                // Lấy UserSurveyResponse bao gồm các UserSurveyAnswers của nó
                var userResponse = await _userSurveyResponseRepository.GetByIdWithAnswersAsync(request.ResponseId);
                if (userResponse == null)
                {
                    return Result<SaveSingleAnswerResponseDto>.NotFound("Không tìm thấy phiên phản hồi khảo sát.");
                }

                var question = await _surveyQuestionRepository.GetSurveyQuestionByIdAsync(request.QuestionId);
                if (question == null)
                {
                    return Result<SaveSingleAnswerResponseDto>.NotFound("Không tìm thấy câu hỏi.");
                }

                var option = await _surveyOptionRepository.GetByIdAsync(request.OptionId);
                if (option == null || option.QuestionId != request.QuestionId)
                {
                    return Result<SaveSingleAnswerResponseDto>.Invalid("Lựa chọn không hợp lệ cho câu hỏi này.");
                }

                UserSurveyAnswer userSurveyAnswer;
                // Kiểm tra xem câu trả lời cho câu hỏi này đã tồn tại trong danh sách của response chưa

                var existingAnswer = userResponse.UserSurveyAnswers
                                       .FirstOrDefault(a => a.QuestionId == request.QuestionId);

                if (existingAnswer != null)
                {
                    // Cập nhật câu trả lời hiện có
                    userSurveyAnswer = existingAnswer;
                    userSurveyAnswer.OptionId = request.OptionId;
                    userSurveyAnswer.AnswerText = option.OptionText;
                    userSurveyAnswer.AnsweredAt = DateTime.Now;
                    await _userSurveyAnswerRepository.UpdateUserSurveyAnswerAsync(userSurveyAnswer);
                }
                else
                {
                    // Thêm câu trả lời mới
                    userSurveyAnswer = new UserSurveyAnswer
                    {
                        ResponseId = request.ResponseId,
                        QuestionId = request.QuestionId,
                        OptionId = request.OptionId,
                        AnswerText = option.OptionText,
                        AnsweredAt = DateTime.Now
                    };
                    userSurveyAnswer = await _userSurveyAnswerRepository.AddNewUserSurveyAnswer(userSurveyAnswer);
                }

                return Result<SaveSingleAnswerResponseDto>.Success(new SaveSingleAnswerResponseDto
                {
                    AnswerId = userSurveyAnswer.AnswerId,
                    ResponseId = userSurveyAnswer.ResponseId,
                    QuestionId = userSurveyAnswer.QuestionId,
                    OptionId = userSurveyAnswer.OptionId!.Value, // Đảm bảo không null vì ta đã kiểm tra option
                    OptionText = option.OptionText,
                    ScoreValue = option.ScoreValue,
                    AnsweredAt = userSurveyAnswer.AnsweredAt,
                    Message = "Câu trả lời đã được lưu thành công." // message for DTO
                }, "Câu trả lời đã được lưu thành công."); // message for Result
            }
            catch (Exception ex)
            {
                return Result<SaveSingleAnswerResponseDto>.Error($"Lỗi khi lưu câu trả lời: {ex.Message}");
            }
        }

        // 3. Phương thức để hoàn thành khảo sát và tính toán kết quả cuối cùng
        public async Task<Result<SurveyResultResponseDto>> CompleteSurvey(Guid responseId)
        {
            try
            {
                // Lấy UserSurveyResponse bao gồm tất cả các UserSurveyAnswers, SurveyOption và Survey
                var userSurveyResponse = await _userSurveyResponseRepository.GetByIdWithAnswersAndSurveyAsync(responseId);

                if (userSurveyResponse == null)
                {
                    return Result<SurveyResultResponseDto>.NotFound("Không tìm thấy phiên phản hồi khảo sát để hoàn thành.");
                }

                // Lấy thông tin Survey để xác định loại khảo sát
                var survey = userSurveyResponse.Survey; // Đã được include từ GetByIdWithAnswersAndSurveyAsync
                if (survey == null)
                {
                    // Fallback nếu survey không được load, nhưng lý tưởng thì nó phải có
                    survey = await _surveyRepository.GetSurveyByIdAsync(userSurveyResponse.SurveyId);
                    if (survey == null)
                    {
                        return Result<SurveyResultResponseDto>.Error("Không tìm thấy thông tin khảo sát liên quan.");
                    }
                }

                int totalScore = 0;
                if (userSurveyResponse.UserSurveyAnswers != null)
                {
                    foreach (var answer in userSurveyResponse.UserSurveyAnswers)
                    {
                        var option = answer.SurveyOption;
                        if (option != null && option.ScoreValue.HasValue)
                        {
                            totalScore += option.ScoreValue.Value;
                        }
                        // Fallback này có thể không cần thiết nếu eager-loading hoạt động đúng
                        else if (answer.OptionId.HasValue)
                        {
                            var fallbackOption = await _surveyOptionRepository.GetByIdAsync(answer.OptionId.Value);
                            if (fallbackOption != null && fallbackOption.ScoreValue.HasValue)
                            {
                                totalScore += fallbackOption.ScoreValue.Value;
                            }
                        }
                    }
                }
                else
                {
                    return Result<SurveyResultResponseDto>.Error("Không thể tải các câu trả lời cho khảo sát.");
                }

                // Tính toán Risk Level và Score Interpretation dựa trên tên khảo sát
                var (riskLevel, scoreInterpretation) = CalculateRiskLevelAndInterpretation(survey.Name, totalScore);

                var recommendedCoursesFromRules = await _surveyCourseRecommendationRepository
                                                    .GetRecommendationsBySurveyAndRiskLevelAsync(userSurveyResponse.SurveyId, riskLevel);

                List<string> recommendedCourseTitles = new List<string>();
                List<UserResponseCourseRecommendation> userSpecificRecommendations = new List<UserResponseCourseRecommendation>();

                foreach (var recRule in recommendedCoursesFromRules)
                {
                    if (recRule.Course != null)
                    {
                        recommendedCourseTitles.Add($"{recRule.Course.Title}");

                        var userRec = new UserResponseCourseRecommendation
                        {
                            ResponseId = userSurveyResponse.ResponseId,
                            CourseId = recRule.CourseId,
                            RecommendedAt = DateTime.Now
                        };
                        userSpecificRecommendations.Add(userRec);
                    }
                }

                // Kiểm tra xem đã có đề xuất nào được lưu cho responseId này chưa để tránh trùng lặp
                var existingUserRecommendations = await _userResponseCourseRecommendationRepository
                                                      .GetUsersResponseByResponseIdAsync(responseId);

                if (!existingUserRecommendations.Any()) // Chỉ thêm nếu chưa có
                {
                    foreach (var userRec in userSpecificRecommendations)
                    {
                        await _userResponseCourseRecommendationRepository.AddUserResponseAsync(userRec);
                    }
                }

                userSurveyResponse.RiskLevel = riskLevel;
                userSurveyResponse.RecommendedAction = string.Join(";", recommendedCourseTitles); // Lưu các tiêu đề khóa học

                await _userSurveyResponseRepository.UpdateAsync(userSurveyResponse);

                return Result<SurveyResultResponseDto>.Success(new SurveyResultResponseDto
                {
                    ResponseId = userSurveyResponse.ResponseId,
                    UserId = userSurveyResponse.UserId,
                    SurveyId = userSurveyResponse.SurveyId,
                    TakenAt = userSurveyResponse.TakenAt,
                    TotalScore = totalScore,
                    RiskLevel = riskLevel,
                    RecommendedActions = recommendedCourseTitles, // Trả về list các tiêu đề
                    Disclaimer = "Lưu ý: Kết quả này chỉ mang tính chất tham khảo và không thay thế cho chẩn đoán chuyên nghiệp. Nếu bạn lo lắng về việc sử dụng chất gây nghiện, vui lòng tham khảo ý kiến của chuyên viên tư vấn.",
                    ScoreInterpretation = scoreInterpretation // Thêm thang điểm đánh giá vào DTO
                }, "Khảo sát đã được hoàn thành và kết quả đã được tính toán.");
            }
            catch (Exception ex)
            {
                return Result<SurveyResultResponseDto>.Error($"Lỗi khi hoàn thành khảo sát: {ex.Message}");
            }
        }

        // 4. Phương thức để lấy lại kết quả đã hoàn thành (nếu người dùng refresh trang kết quả)
        public async Task<Result<SurveyResultResponseDto>> GetSurveyResult(Guid responseId)
        {
            try
            {
                // Lấy UserSurveyResponse cùng với UserSurveyAnswers và các option
                var userSurveyResponse = await _userSurveyResponseRepository.GetByIdWithAnswersAndSurveyAsync(responseId);

                if (userSurveyResponse == null)
                {
                    return Result<SurveyResultResponseDto>.NotFound("Không tìm thấy kết quả khảo sát.");
                }

                // Lấy thông tin Survey để xác định loại khảo sát
                var survey = userSurveyResponse.Survey;
                if (survey == null)
                {
                    survey = await _surveyRepository.GetSurveyByIdAsync(userSurveyResponse.SurveyId);
                    if (survey == null)
                    {
                        return Result<SurveyResultResponseDto>.Error("Không tìm thấy thông tin khảo sát liên quan.");
                    }
                }

                if (string.IsNullOrEmpty(userSurveyResponse.RiskLevel) || string.IsNullOrEmpty(userSurveyResponse.RecommendedAction))
                {
                    return Result<SurveyResultResponseDto>.Invalid("Khảo sát này chưa được hoàn thành hoặc kết quả chưa được tính toán.");
                }

                var recommendedActionsList = userSurveyResponse.RecommendedAction.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

                // Re-calculate total score to ensure consistency (optional, but good for validation)
                int totalScore = 0;
                if (userSurveyResponse.UserSurveyAnswers != null)
                {
                    foreach (var answer in userSurveyResponse.UserSurveyAnswers)
                    {
                        var option = answer.SurveyOption;
                        if (option != null && option.ScoreValue.HasValue)
                        {
                            totalScore += option.ScoreValue.Value;
                        }
                    }
                }

                // Tính toán Risk Level và Score Interpretation dựa trên tên khảo sát
                // Sử dụng lại logic từ CompleteSurvey để đảm bảo tính nhất quán
                var (riskLevel, scoreInterpretation) = CalculateRiskLevelAndInterpretation(survey.Name, totalScore);

                return Result<SurveyResultResponseDto>.Success(new SurveyResultResponseDto
                {
                    ResponseId = userSurveyResponse.ResponseId,
                    UserId = userSurveyResponse.UserId,
                    SurveyId = userSurveyResponse.SurveyId,
                    TakenAt = userSurveyResponse.TakenAt,
                    TotalScore = totalScore,
                    RiskLevel = userSurveyResponse.RiskLevel, // Lấy từ DB nếu đã lưu
                    RecommendedActions = recommendedActionsList,
                    Disclaimer = "Lưu ý: Kết quả này chỉ mang tính chất tham khảo và không thay thế cho chẩn đoán chuyên nghiệp. Nếu bạn lo lắng về việc sử dụng chất gây nghiện, vui lòng tham khảo ý kiến của chuyên viên tư vấn.",
                    ScoreInterpretation = scoreInterpretation // Thêm thang điểm đánh giá vào DTO
                }, "Lấy kết quả khảo sát thành công.");
            }
            catch (Exception ex)
            {
                return Result<SurveyResultResponseDto>.Error($"Lỗi khi lấy kết quả khảo sát: {ex.Message}");
            }
        }

        private (string riskLevel, List<string> scoreInterpretation) CalculateRiskLevelAndInterpretation(string surveyName, int totalScore)
        {
            string riskLevel = "Không xác định";
            List<string> scoreInterpretation = new List<string>();

            switch (surveyName)
            {
                case "Bài đánh giá ASSIST":
                    if (totalScore >= 0 && totalScore <= 3)
                    {
                        riskLevel = "Nguy cơ thấp";
                    }
                    else if (totalScore >= 4 && totalScore <= 26)
                    {
                        riskLevel = "Nguy cơ trung bình";
                    }
                    else if (totalScore >= 27)
                    {
                        riskLevel = "Nguy cơ cao";
                    }
                    scoreInterpretation.Add("Thang điểm đánh giá ASSIST:");
                    scoreInterpretation.Add("0-3: Nguy cơ thấp");
                    scoreInterpretation.Add("4-26: Nguy cơ trung bình");
                    scoreInterpretation.Add("27+: Nguy cơ cao");
                    break;
                case "Bài đánh giá CRAFFT":
                    // Dựa trên ảnh image_07107b.png
                    if (totalScore == 0)
                    {
                        riskLevel = "Không có nguy cơ";
                    }
                    else if (totalScore == 1)
                    {
                        riskLevel = "Nguy cơ thấp";
                    }
                    else if (totalScore == 2)
                    {
                        riskLevel = "Nguy cơ trung bình";
                    }
                    else if (totalScore >= 3) // 3+
                    {
                        riskLevel = "Nguy cơ cao";
                    }
                    scoreInterpretation.Add("Thang điểm đánh giá CRAFFT:");
                    scoreInterpretation.Add("0: Không có nguy cơ");
                    scoreInterpretation.Add("1: Nguy cơ thấp");
                    scoreInterpretation.Add("2: Nguy cơ trung bình");
                    scoreInterpretation.Add("3+: Nguy cơ cao");
                    break;
                case "Bài đánh giá AUDIT":
                    // Thêm logic đánh giá cho AUDIT nếu có
                    // Ví dụ:
                    // if (totalScore >= 0 && totalScore <= 7) riskLevel = "Nguy cơ thấp";
                    // else if (totalScore >= 8 && totalScore <= 15) riskLevel = "Nguy cơ trung bình";
                    // else riskLevel = "Nguy cơ cao";
                    // scoreInterpretation.Add("Thang điểm đánh giá AUDIT:");
                    // scoreInterpretation.Add("0-7: Nguy cơ thấp");
                    // scoreInterpretation.Add("8-15: Nguy cơ trung bình");
                    // scoreInterpretation.Add("16+: Nguy cơ cao");
                    break;
                default:
                    // Mặc định hoặc xử lý cho các khảo sát không xác định
                    riskLevel = "Không xác định";
                    scoreInterpretation.Add("Không có thang điểm đánh giá cụ thể cho khảo sát này.");
                    break;
            }
            return (riskLevel, scoreInterpretation);
        }

        public async Task<Result<List<SurveyResultSummaryResponse>>> GetSurveyResultsByUserId(Guid userId)
        {
            try
            {
                // Lấy tất cả phản hồi khảo sát của người dùng cùng với thông tin khảo sát
                var userResponses = await _userSurveyResponseRepository.GetByUserIdWithSurveyAsync(userId);
                if (userResponses == null || !userResponses.Any())
                {
                    return Result<List<SurveyResultSummaryResponse>>.NotFound("Không tìm thấy kết quả khảo sát nào cho người dùng này.");
                }

                var surveyList = new List<SurveyResultSummaryResponse>();

                foreach (var response in userResponses)
                {
                    if (string.IsNullOrEmpty(response.RiskLevel) || string.IsNullOrEmpty(response.RecommendedAction))
                    {
                        continue;
                    }

                    var recommendedActionsList = response.RecommendedAction.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

                    // Tính toán totalScore từ UserSurveyAnswers đã được tải sẵn
                    int totalScore = 0;
                    if (response.UserSurveyAnswers != null)
                    {
                        foreach (var answer in response.UserSurveyAnswers)
                        {
                            // SurveyOption đã được ThenInclude
                            if (answer.SurveyOption != null && answer.SurveyOption.ScoreValue.HasValue)
                            {
                                totalScore += answer.SurveyOption.ScoreValue.Value;
                            }
                        }
                    }
                    // else: không cần fallback ở đây vì repository đảm bảo UserSurveyAnswers được tải

                    surveyList.Add(new SurveyResultSummaryResponse
                    {
                        ResponseId = response.ResponseId,
                        SurveyId = response.SurveyId,
                        SurveyName = response.Survey?.Name ?? "N/A",
                        TakenAt = response.TakenAt,
                        TotalScore = totalScore, // Dùng TotalScore đã tính lại
                        RiskLevel = response.RiskLevel,
                        RecommendedActions = recommendedActionsList
                    });
                }

                return Result<List<SurveyResultSummaryResponse>>.Success(surveyList, "Lấy danh sách kết quả khảo sát thành công.");

            }
            catch (Exception ex)
            {
                return Result<List<SurveyResultSummaryResponse>>.Error($"Lỗi khi lấy danh sách kết quả khảo sát: {ex.Message}");
            }
        }

        public async Task<Result<List<SurveyQuestionAnswerResponse>>> GetSurveyQuestionAnswersByResponseIdAsync(Guid responseId)
        {
            try
            {
                var userSurveyResponse = await _userSurveyResponseRepository.GetByIdWithAnswersAndSurveyAsync(responseId);

                if (userSurveyResponse == null)
                {
                    return Result<List<SurveyQuestionAnswerResponse>>.NotFound("Không tìm thấy kết quả khảo sát với ID này.");
                }

                if (userSurveyResponse.UserSurveyAnswers == null || !userSurveyResponse.UserSurveyAnswers.Any())
                {
                    return Result<List<SurveyQuestionAnswerResponse>>.NotFound("Không có câu trả lời nào được tìm thấy cho phản hồi này.");
                }

                var questionAnswersList = userSurveyResponse.UserSurveyAnswers
                    .Where(a => a.SurveyQuestion != null && a.SurveyOption != null)
                    .OrderBy(a => a.SurveyQuestion!.Sequence)
                    .Select(a => new SurveyQuestionAnswerResponse
                    {
                        QuestionText = a.SurveyQuestion!.QuestionText,
                        ChosenAnswerText = a.SurveyOption!.OptionText, 
                        Score = a.SurveyOption.ScoreValue
                    })
                    .ToList();

                return Result<List<SurveyQuestionAnswerResponse>>.Success(questionAnswersList, "Lấy chi tiết câu hỏi và câu trả lời thành công.");
            }
            catch (Exception ex)
            {
                return Result<List<SurveyQuestionAnswerResponse>>.Error($"Lỗi khi lấy chi tiết câu hỏi và câu trả lời: {ex.Message}");
            }
        }
    }
}