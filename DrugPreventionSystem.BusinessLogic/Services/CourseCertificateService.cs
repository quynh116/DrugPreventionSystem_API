using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CourseCertificateService : ICourseCertificateService
    {
        private readonly ICourseCertificateRepository _repo;
        public CourseCertificateService(ICourseCertificateRepository repo)
        {
            _repo = repo;
        }
        private CourseCertificateResponse MapToResponse(CourseCertificate entity)
        {
            return new CourseCertificateResponse
            {
                CertificateId = entity.CertificateId,
                UserId = entity.UserId,
                CourseId = entity.CourseId,
                IssuedAt = entity.IssuedAt,
                CertificateUrl = entity.CertificateUrl
            };
        }
        public async Task<Result<CourseCertificateResponse>> AddAsync(CourseCertificateRequest request)
        {
            var entity = new CourseCertificate
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                IssuedAt = request.IssuedAt ?? DateTime.Now,
                CertificateUrl = request.CertificateUrl
            };
            var added = await _repo.AddAsync(entity);
            return Result<CourseCertificateResponse>.Success(MapToResponse(added), "Added successfully");
        }
        public async Task<Result<IEnumerable<CourseCertificateResponse>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return Result<IEnumerable<CourseCertificateResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<CourseCertificateResponse>> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<CourseCertificateResponse>.NotFound($"Not found CourseCertificate with id: {id}");
            return Result<CourseCertificateResponse>.Success(MapToResponse(entity));
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }
        public async Task<Result<CourseCertificateResponse>> UpdateAsync(Guid id, CourseCertificateRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<CourseCertificateResponse>.NotFound($"Not found CourseCertificate with id: {id}");
            entity.UserId = request.UserId;
            entity.CourseId = request.CourseId;
            entity.IssuedAt = request.IssuedAt ?? entity.IssuedAt;
            entity.CertificateUrl = request.CertificateUrl;
            await _repo.UpdateAsync(entity);
            return Result<CourseCertificateResponse>.Success(MapToResponse(entity), "Updated successfully");
        }
        public async Task<Result<IEnumerable<CourseCertificateResponse>>> GetByUserIdAsync(Guid userId)
        {
            var list = await _repo.GetByUserIdAsync(userId);
            return Result<IEnumerable<CourseCertificateResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<IEnumerable<CourseCertificateResponse>>> GetByCourseIdAsync(Guid courseId)
        {
            var list = await _repo.GetByCourseIdAsync(courseId);
            return Result<IEnumerable<CourseCertificateResponse>>.Success(list.Select(MapToResponse));
        }
    }
} 