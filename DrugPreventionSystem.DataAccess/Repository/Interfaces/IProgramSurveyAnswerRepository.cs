using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface IProgramSurveyAnswerRepository
    {
        Task<List<ProgramSurveyAnswer>> GetByResponseIdAsync(Guid responseId);
        Task<ProgramSurveyAnswer?> GetByIdAsync(Guid answerId);
        Task AddAsync(ProgramSurveyAnswer answer);
        Task UpdateAsync(ProgramSurveyAnswer answer);
        Task DeleteAsync(Guid answerId);
    }
}
