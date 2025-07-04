﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ISurveyQuestionService
    {
        Task<Result<SurveyQuestionResponse>> AddSurveyQuestionAsync(SurveyQuestionCreateRequest request);
        Task<Result<IEnumerable<SurveyQuestionResponse>>> GetAllSurveyQuestionsAsync();
        Task<Result<SurveyQuestionResponse>> GetSurveyQuestionByIdAsync(Guid surveyQuestionId);
        Task<Result<IEnumerable<SurveyQuestionResponse>>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId);
        Task<Result<SurveyQuestionResponse>> UpdateSurveyQuestionAsync(SurveyQuestionUpdateRequest request, Guid questionId);
        Task<Result<bool>> DeleteSurveyQuestionAsync(Guid questionId);

    }
}
