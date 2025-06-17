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
    public class UserSurveyResponseService :IUserSurveyResponseService
    {
        private readonly IUserSurveyResponseRepository _userSurveyResponseRepository;
        private readonly IUserSurveyAnswerRepository _userSurveyAnswerRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyOptionRepository _surveyOptionRepository;

        public UserSurveyResponseService(IUserSurveyResponseRepository userSurveyResponseRepository, IUserSurveyAnswerRepository userSurveyAnswerRepository,
            ISurveyRepository surveyRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            ISurveyOptionRepository surveyOptionRepository)
        {
            _userSurveyResponseRepository = userSurveyResponseRepository;
            _userSurveyAnswerRepository = userSurveyAnswerRepository;
            _surveyRepository = surveyRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyOptionRepository = surveyOptionRepository;

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
        public async Task<Result<bool>>DeleteAsync(Guid id)
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
                // Lấy UserSurveyResponse bao gồm tất cả các UserSurveyAnswers và SurveyOption của chúng
                var userSurveyResponse = await _userSurveyResponseRepository.GetByIdWithAnswersAsync(responseId);

                if (userSurveyResponse == null)
                {
                    return Result<SurveyResultResponseDto>.NotFound("Không tìm thấy phiên phản hồi khảo sát để hoàn thành.");
                }

                int totalScore = 0;
                // Đảm bảo UserSurveyAnswers được load. Nếu GetByIdWithAnswersAsync đã load, nó sẽ không null
                if (userSurveyResponse.UserSurveyAnswers != null)
                {
                    foreach (var answer in userSurveyResponse.UserSurveyAnswers)
                    {
                        // `SurveyOption` nên đã được eager-loaded bởi `GetByIdWithAnswersAsync`
                        var option = answer.SurveyOption;
                        if (option != null && option.ScoreValue.HasValue)
                        {
                            totalScore += option.ScoreValue.Value;
                        }
                        // Fallback: if not loaded, get from DB (less efficient)
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
                    // Trường hợp này không nên xảy ra nếu GetByIdWithAnswersAsync hoạt động đúng
                    return Result<SurveyResultResponseDto>.Error("Không thể tải các câu trả lời cho khảo sát.");
                }


                // Tính toán Risk Level và Recommended Actions dựa trên TotalScore (logic ASSIST)
                string riskLevel;
                List<string> recommendedActions = new List<string>();

                if (totalScore >= 0 && totalScore <= 3)
                {
                    riskLevel = "Nguy cơ thấp";
                    recommendedActions.Add("Duy trì hành vi hiện tại và nâng cao nhận thức về tác hại của chất gây nghiện.");
                }
                else if (totalScore >= 4 && totalScore <= 26)
                {
                    riskLevel = "Nguy cơ trung bình";
                    recommendedActions.Add("Tham gia khóa học về nhận thức ma túy.");
                    recommendedActions.Add("Tham khảo ý kiến chuyên viên tư vấn để có hướng dẫn cụ thể.");
                    recommendedActions.Add("Giảm thiểu việc sử dụng chất gây nghiện một cách có ý thức.");
                    recommendedActions.Add("Tìm hiểu và thực hành các kỹ năng từ chối và đối phó với áp lực.");
                }
                else if (totalScore >= 27)
                {
                    riskLevel = "Nguy cơ cao";
                    recommendedActions.Add("Tham gia khóa học về nhận thức ma túy chuyên sâu.");
                    recommendedActions.Add("Tham gia liệu pháp tâm lý và các chương trình hỗ trợ cai nghiện.");
                    recommendedActions.Add("Được giới thiệu đến các dịch vụ điều trị nghiện chuyên sâu.");
                    recommendedActions.Add("Nhận hỗ trợ xã hội và gia đình để tăng cường khả năng phục hồi.");
                }
                else
                {
                    riskLevel = "Không xác định";
                    recommendedActions.Add("Đã xảy ra lỗi trong quá trình tính toán điểm. Vui lòng liên hệ hỗ trợ.");
                }

                userSurveyResponse.RiskLevel = riskLevel;
                userSurveyResponse.RecommendedAction = string.Join(";", recommendedActions); // Lưu vào DB dưới dạng chuỗi

                await _userSurveyResponseRepository.UpdateAsync(userSurveyResponse);

                return Result<SurveyResultResponseDto>.Success(new SurveyResultResponseDto
                {
                    ResponseId = userSurveyResponse.ResponseId,
                    UserId = userSurveyResponse.UserId,
                    SurveyId = userSurveyResponse.SurveyId,
                    TakenAt = userSurveyResponse.TakenAt,
                    TotalScore = totalScore,
                    RiskLevel = riskLevel,
                    RecommendedActions = recommendedActions,
                    Disclaimer = "Lưu ý: Kết quả này chỉ mang tính chất tham khảo và không thay thế cho chẩn đoán chuyên nghiệp. Nếu bạn lo lắng về việc sử dụng chất gây nghiện, vui lòng tham khảo ý kiến của chuyên viên tư vấn."
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
                var userSurveyResponse = await _userSurveyResponseRepository.GetByIdWithAnswersAsync(responseId);

                if (userSurveyResponse == null)
                {
                    return Result<SurveyResultResponseDto>.NotFound("Không tìm thấy kết quả khảo sát.");
                }
                if (string.IsNullOrEmpty(userSurveyResponse.RiskLevel) || string.IsNullOrEmpty(userSurveyResponse.RecommendedAction))
                {
                    // Điều này có nghĩa là khảo sát chưa được hoàn thành
                    return Result<SurveyResultResponseDto>.Invalid("Khảo sát này chưa được hoàn thành hoặc kết quả chưa được tính toán.");
                }

                // Reconstruct RecommendedActions list from the string stored in DB
                var recommendedActionsList = userSurveyResponse.RecommendedAction.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

                // Re-calculate total score to ensure consistency (optional, but good for validation)
                int totalScore = 0;
                if (userSurveyResponse.UserSurveyAnswers != null)
                {
                    foreach (var answer in userSurveyResponse.UserSurveyAnswers)
                    {
                        var option = answer.SurveyOption; // Should be loaded by GetByIdWithAnswersAsync
                        if (option != null && option.ScoreValue.HasValue)
                        {
                            totalScore += option.ScoreValue.Value;
                        }
                    }
                }

                return Result<SurveyResultResponseDto>.Success(new SurveyResultResponseDto
                {
                    ResponseId = userSurveyResponse.ResponseId,
                    UserId = userSurveyResponse.UserId,
                    SurveyId = userSurveyResponse.SurveyId,
                    TakenAt = userSurveyResponse.TakenAt,
                    TotalScore = totalScore,
                    RiskLevel = userSurveyResponse.RiskLevel,
                    RecommendedActions = recommendedActionsList,
                    Disclaimer = "Lưu ý: Kết quả này chỉ mang tính chất tham khảo và không thay thế cho chẩn đoán chuyên nghiệp. Nếu bạn lo lắng về việc sử dụng chất gây nghiện, vui lòng tham khảo ý kiến của chuyên viên tư vấn."
                }, "Lấy kết quả khảo sát thành công.");
            }
            catch (Exception ex)
            {
                return Result<SurveyResultResponseDto>.Error($"Lỗi khi lấy kết quả khảo sát: {ex.Message}");
            }
        }
    }
}