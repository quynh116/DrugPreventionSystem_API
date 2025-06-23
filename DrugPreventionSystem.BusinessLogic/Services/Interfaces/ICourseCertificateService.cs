using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ICourseCertificateService
    {
        Task<Result<CourseCertificateResponse>> AddAsync(CourseCertificateRequest request);
        Task<Result<IEnumerable<CourseCertificateResponse>>> GetAllAsync();
        Task<Result<CourseCertificateResponse>> GetByIdAsync(Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<CourseCertificateResponse>> UpdateAsync(Guid id, CourseCertificateRequest request);
        Task<Result<IEnumerable<CourseCertificateResponse>>> GetByUserIdAsync(Guid userId);
        Task<Result<IEnumerable<CourseCertificateResponse>>> GetByCourseIdAsync(Guid courseId);
    }
} 