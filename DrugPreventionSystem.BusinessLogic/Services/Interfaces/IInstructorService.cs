using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Instructor;
using DrugPreventionSystem.BusinessLogic.Models.Responses;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<Result<InstructorResponse>> CreateAsync(InstructorCreateRequest request);
        Task<Result<IEnumerable<InstructorResponse>>> GetAllAsync();
        Task<Result<InstructorResponse>> GetByIdAsync(Guid id);
        Task<Result<InstructorResponse>> UpdateAsync(InstructorUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);

    }
}
