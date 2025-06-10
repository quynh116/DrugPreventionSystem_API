using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IConsultantService
    {
        Task<Result<IEnumerable<ConsultantReadResponse>>> GetAllConsultantAsync();
        Task<Result<ConsultantReadResponse>> GetConsultantByIdAsync(Guid consultantId);
        Task<Result<ConsultantUpdateResponse>> UpdateConsultantAsync(Guid consultantId, ConsultantUpdateRequest request);
        Task<Result<bool>> DeleteConsultantAsync(Guid id);
    }
}
